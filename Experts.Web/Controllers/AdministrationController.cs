using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Experts.Core.Entities;
using Experts.Core.Entities.Events;
using Experts.Core.Utils;
using Experts.Web.Filters;
using Experts.Web.Helpers;
using Experts.Web.Models.Administration;
using Experts.Web.Models.Events;
using Experts.Web.Models.Forms;
using Experts.Web.Models.Shared;
using System;
using Experts.Web.Utils;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid;
using Experts.Core.Repositories;

namespace Experts.Web.Controllers
{
    [AuthorizeRoles(Role.Moderator)]
    public partial class AdministrationController : BaseController
    {
        [DefaultRouting]
        public virtual ActionResult ModeratorMenu()
        {
            /*var model = new ModeratorMenu();

            Inclue Moderator Notification Code Model*/          

            return PartialView(MVC.Administration.Views._TopMenuModerator);
        }

        public virtual ActionResult EventsLog([QueryParameter]int? page = null, [QueryParameter] int? threadId = null)
        {
            TODO: //do wyciepki threadId -> zdarzenia specyficznego threada sa w widoku dla tegóż wątku

            Func<IQueryable<Event>, IQueryable<Event>> query = null;
            /*if (threadId.HasValue)
                query = e => e.ByThread(threadId.Value);*/

            query = e => e.ByHideOnMainList(false);

            var logs = Repository.Event.Find(PagerHelper.PageSize, page ?? 1, query, e => e.OccurenceDate, false, e => e.IsHandled);
            var count = Repository.Event.Count(query);
            var pagination = PagerHelper.Pagination(page, count);
            var model = new SortableGridModel<Event> { Data = logs, Pagination = pagination };

            return View(model);
        }

        [DefaultRouting]
        public virtual ActionResult EventReactionForm(int eventId)
        {
            var systemEvent = Repository.Event.Get(eventId);
            var genericType = EventLog.GetReactionType(systemEvent.GetType());

            var reactionModel = Activator.CreateInstance(genericType);

            genericType.GetProperty("Event").SetValue(reactionModel, systemEvent, null);
            genericType.GetProperty("EventId").SetValue(reactionModel, eventId, null);

            genericType.GetMethod("Initialize").Invoke(reactionModel, null);

            var viewName = (string)genericType.GetProperty("ViewName").GetValue(reactionModel, null);

            return PartialView(viewName, reactionModel);
        }

        [DefaultRouting]
        [EventReaction]
        [HttpPost]
        public virtual ActionResult SystemBreakdownReaction(SystemBreakdownReactionModel model)
        {
            return null;
        }

        [DefaultRouting]
        [EventReaction]
        [HttpPost]
        public virtual ActionResult UserFailureReaction(UserFailureReactionModel model)
        {
            return null;
        }

        [DefaultRouting]
        [EventReaction]
        [HttpPost]
        public virtual ActionResult ExpertQualificationsChangedReaction(ExpertQualificationsChangedReactionModel model)
        {
            if (!ModelState.IsValid)
                return View(model.ViewName, model);

            var relatedEvent = (ExpertQualificationsChangedEvent)Repository.Event.Get(model.EventId);
            var expert = relatedEvent.RelatedUser.Expert;

            expert.VerificationStatus = model.IsAccepted ? ExpertVerificationStatus.Verified : ExpertVerificationStatus.Rejected;
            expert.VerificationDetails = model.VerificationDetails;

            Repository.Expert.Update(expert);

            return null;
        }

        [DefaultRouting]
        [EventReaction]
        [HttpPost]
        public virtual ActionResult ThreadIssueReaction(ThreadIssueReactionModel model)
        {
            if (!ModelState.IsValid)
                return View(model.ViewName, model);

            var relatedEvent = (ThreadIssueReportEvent) Repository.Event.Get(model.EventId);
            var thread = relatedEvent.RelatedThread;
            var changedCategory = Repository.Category.Get(model.ChosenCategoryId);

            if(model.RemoveDuplication)
            {
                thread.State = ThreadState.Closed; //closed by moderator?
                Repository.Thread.AddPost(thread, SystemPostFactory.BuildClosedByModeratorPost(AuthenticationHelper.CurrentUser.Moderator, model.IssueDetails));
            }

            if(changedCategory != null && thread.Category != changedCategory)
            {
                thread.Category = changedCategory;
                Repository.Thread.AddPost(thread, SystemPostFactory.BuildChangedCategoryByModeratorPost(AuthenticationHelper.CurrentUser.Moderator, model.IssueDetails));
            }

            Repository.Thread.Update(thread);

            return null;
        }

        [DefaultRouting]
        [EventReaction]
        [HttpPost]
        public virtual ActionResult CashPaymentReaction(CashPaymentReactionModel model)
        {
            if (!ModelState.IsValid)
                return View(model.ViewName, model);


            return null;
        }

        [DefaultRouting]
        [EventReaction]
        [HttpPost]
        public virtual ActionResult ExpertPublicDataChangedReaction(CashPaymentReactionModel model)
        {
            if (!ModelState.IsValid)
                return View(model.ViewName, model);


            return null;
        }

        [DefaultRouting]
        [EventReaction]
        [HttpPost]
        public virtual ActionResult NoAnswerReaction(NoAnswerReactionModel model)
        {
            if (!ModelState.IsValid)
                return View(model.ViewName, model);

            //do some stuff

            return null;
        }

        [DefaultRouting]
        [EventReaction]
        [HttpPost]
        public virtual ActionResult ExpertPriceProposalReaction(ExpertPriceProposalReactionModel model)
        {
            if (!ModelState.IsValid)
                return View(model.ViewName, model);

            model.Event = Repository.Event.Get(model.EventId) as ExpertPriceProposalEvent;
            model.PriceProposal = Repository.Thread.GetPriceProposal(model.Event.AdditionalId);

            var thread = model.Event.RelatedThread;
            var priceProposal = model.PriceProposal;

            if(model.VerificationStatus == PriceProposalVerificationStatus.Declined)
            {
                priceProposal.VerificationStatus = PriceProposalVerificationStatus.Declined;
                Repository.Thread.DeletePriceProposal(priceProposal);
            }

            if (model.VerificationStatus == PriceProposalVerificationStatus.Verified)
            {
                priceProposal.VerificationStatus = PriceProposalVerificationStatus.Verified;
                if (priceProposal.Thread.CreationDate.AddHours(24) <= DateTime.Now)
                {
                    Email.Send<PriceProposalEmail>(priceProposal.Thread, priceProposal.Expert);
                }
            }

            if (model.VerificationStatus == PriceProposalVerificationStatus.Changed)
            {
                priceProposal.VerificationStatus = PriceProposalVerificationStatus.Changed;
                thread.CalculateAdditionalExpertProvision(
                    ThreadValueHelper.CalculateValueBasedOnExpertLevel(priceProposal.SurchargeValue, priceProposal.Expert));

                if (priceProposal.Thread.CreationDate.AddHours(24) <= DateTime.Now)
                {
                    Email.Send<PriceProposalEmail>(priceProposal.Thread, priceProposal.Expert);
                }
            }

            Repository.Thread.Update(thread);

            return null;
        }


        [DefaultRouting]
        [EventReaction]
        [HttpPost]
        public virtual ActionResult NewAdditionalServiceReaction(NewAdditionalServiceReactionModel model)
        {
            if (model.IsVerified)
            {
                var additionalService = Repository.Thread.GetAdditionalService(model.AdditionalService.Id);
                additionalService.IsVerified = true;
                Repository.Thread.UpdateThreadByAdditionalService(additionalService);
            }

            return null;
        }

        [DefaultRouting]
        [EventReaction]
        [HttpPost]
        public virtual ActionResult BecomePartnerRequestReaction(BecomePartnerRequestReactionModel model)
        {
            model.Event = (BecomePartnerRequestEvent) Repository.Event.Get(model.EventId);
            var user = model.Event.RelatedUser;

            if (model.Accept)
            {
                Repository.Partner.Create(user);
                Email.Send<PartnerAcceptedEmail>(user);
            }
            else
            {
                Email.Send<PartnerRejectedEmail>(user);
            }

            return null;
        }

        public virtual ActionResult UserList([QueryParameter]GridSortOptions sortOptions = null, [QueryParameter]int? page = null)
        {
            var model = GetUserListModel(sortOptions, page);
            return View(model);
        }

        [AuthorizeRoles(Role.Moderator)]
        public virtual ActionResult CategoryList()
        {
            return View(Repository.Category.All());
        }

        private SortableGridModel<User> GetUserListModel(GridSortOptions sortOptions, int? page)
        {            
            if (sortOptions == null)
                sortOptions = new GridSortOptions();

            if (sortOptions.Column == null)
            {
                sortOptions.Column = GridHelper.UserOrder.Email;
                sortOptions.Direction = SortDirection.Ascending;
            }

            Func<User, IComparable> order = u => GridHelper.UserOrder.ColumnOrders[sortOptions.Column](u);

            var userList = Repository.User.Find(order: order,
                                                ascending: sortOptions.Direction == SortDirection.Ascending,
                                                itemsPerPage: PagerHelper.PageSize, page: page ?? 1);

            var userListCount = Repository.User.Count();

            return new SortableGridModel<User>
                       {
                           Data = userList,
                           SortOptions = sortOptions,
                           Pagination = PagerHelper.Pagination(page, userListCount)
                       };
        }

        public virtual ActionResult CreateCategoryAttribute(int categoryId)
        {
            var model = new CategoryAttributeForm {CategoryId = categoryId};
            return View(model);
        }

        public virtual ActionResult CreateChildCategoryAttribute(int parentCategoryAttributeId)
        {
            var parentAttribute = Repository.Category.GetCategoryAttribute(parentCategoryAttributeId);
            var model = new CategoryAttributeForm
                {
                    ParentAttributeId = parentCategoryAttributeId,
                    AvailableParentOptions = parentAttribute.Options
                };
            return View(MVC.Administration.Views.CreateCategoryAttribute, model);
        }

        [HttpPost]
        public virtual ActionResult CreateCategoryAttribute(CategoryAttributeForm form)
        {
            if (!ModelState.IsValid)
                return View(form);

            var categoryAttribute = Mapper.Map<CategoryAttribute>(form);

            if (form.ParentAttributeId.HasValue)
            {
                var parentAttribute = Repository.Category.GetCategoryAttribute(form.ParentAttributeId.Value);
                parentAttribute.ChildAttributes.Add(categoryAttribute);
                Repository.Category.UpdateCategoryAttribute(parentAttribute);
            }
            else
            {
                var category = Repository.Category.Get(form.CategoryId);
                category.Attributes.Add(categoryAttribute);
                Repository.Category.Update(category);
            }

            Flash.Success(Resources.Administration.CategoryAttributeCreated);

            if (form.Type == CategoryAttributeType.SingleSelect || form.Type == CategoryAttributeType.MultiSelect)
                return RedirectToAction(MVC.Administration.EditCategoryAttribute(categoryAttribute.Id, form.ParentAttributeId));

            return RedirectToAction(MVC.Administration.CategoryList());
        }

        public virtual ActionResult EditCategoryAttribute(int attributeId, int? parentAttributeId = null)
        {
            var attribute = Repository.Category.GetCategoryAttribute(attributeId);
            var model = Mapper.Map<CategoryAttributeForm>(attribute);
            model.ParentAttributeId = parentAttributeId;

            if (parentAttributeId.HasValue)
                model.AvailableParentOptions = Repository.Category.GetCategoryAttribute(parentAttributeId.Value).Options;

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult EditCategoryAttribute(CategoryAttributeForm form)
        {
            if (!ModelState.IsValid)
                return View(form);

            var attribute = Repository.Category.GetCategoryAttribute(form.AttributeId);
            attribute.ParentOptions.Clear();

            Mapper.Map(form, attribute);

            Repository.Category.UpdateCategoryAttribute(attribute);

            Flash.Success(Resources.Administration.CategoryAttributeUpdated);
            return RedirectToAction(MVC.Administration.CategoryList());
        }

        public virtual ActionResult DeleteCategoryAttribute(int attributeId)
        {
            var categoryAttribute = Repository.Category.GetCategoryAttribute(attributeId);
            Repository.Category.DeleteCategoryAttribute(categoryAttribute);

            Flash.Success(Resources.Administration.CategoryAttributeDeleted);

            return RedirectToAction(MVC.Administration.CategoryList());
        }

        [DefaultRouting]
        public virtual ActionResult DeleteCategoryAttributeOption(int optionId)
        {
            Repository.Category.DeleteCategoryAttributeOption(optionId);
            return null;
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult AddCategoryAttributeOption(int categoryAttributeId, CategoryAttributeOption option)
        {
            var categoryAttribute = Repository.Category.GetCategoryAttribute(categoryAttributeId);
            categoryAttribute.Options.Add(option);
            Repository.Category.UpdateCategoryAttribute(categoryAttribute);

            var model = Mapper.Map<CategoryAttributeForm>(categoryAttribute);
            return PartialView(MVC.Administration.Views._CategoryAttributeOptionsList, model);
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult UpdateCategoryAttributeOption(int categoryAttributeId, CategoryAttributeOption updatedOption)
        {
            var categoryAttribute = Repository.Category.GetCategoryAttribute(categoryAttributeId);

            var option = categoryAttribute.Options.Single(o => o.Id == updatedOption.Id);
            option.Value = updatedOption.Value;
            option.PriceModifier = updatedOption.PriceModifier;
            Repository.Category.UpdateCategoryAttribute(categoryAttribute);

            var model = Mapper.Map<CategoryAttributeForm>(categoryAttribute);
            return PartialView(MVC.Administration.Views._CategoryAttributeOptionsList, model);
        }
    }
}
