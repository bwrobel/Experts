using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using C5;
using Experts.Core.Entities;
using Experts.Core.Entities.Events;
using Experts.Core.Exceptions;
using Experts.Core.Utils;
using Experts.Web.Filters;
using Experts.Web.Models.Account;
using Experts.Web.Models.Balance;
using Experts.Web.Models.Feedback;
using Experts.Web.Models.Forms;
using Experts.Web.Helpers;
using System.Linq;
using Experts.Core.Repositories;
using Experts.Web.Models.Profile;
using Experts.Web.Models.Shared;
using Experts.Web.Models.Threads;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid;

namespace Experts.Web.Controllers
{
    public partial class ProfileController : BaseController
    {
        [AuthorizeSignedUpUser]
        public virtual ActionResult Edit()
        {
            return View(Mapper.Map<ProfileForm>(AuthenticationHelper.CurrentUser));
        }

        [AuthorizeSignedUpUser]
        [HttpPost]
        public virtual ActionResult EditEmail([Bind(Prefix = "EmailForm")] EmailForm model)
        {
            if (!ModelState.IsValid)
            {
                var profile = Mapper.Map<ProfileForm>(AuthenticationHelper.CurrentUser);
                profile.EmailForm = model;
                return View(MVC.Profile.Views.Edit, profile);
            }

            var user = AuthenticationHelper.CurrentUser;

            user.NewEmail = model.Email;
            user.GenerateActivationKey();


            Repository.User.Update(user);

            Log.Event<UserChangedEmailEvent>(user, user.NewEmail);
            Email.Send<ConfirmationEmail>(user);

            Flash.Info(Resources.Account.EmailChanged);
            return RedirectToAction(MVC.Profile.Edit());
        }

        [AssignMetadata]
        [AuthorizeSignedUpUser]
        [HttpPost]
        public virtual ActionResult EditPassword([Bind(Prefix = "PasswordForm")] PasswordForm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = Repository.User.VerifyCredentials(AuthenticationHelper.CurrentUser.Email, model.OldPassword);
                    Repository.User.ChangePassword(user, model.Password);

                    Log.Event<UserChangedPasswordEvent>(user);

                    Flash.Success(Resources.Account.PasswordChanged);
                    return RedirectToAction(MVC.Profile.Edit());
                }
                catch (Exception)
                {
                    ModelState.AddModelError("PasswordForm.OldPassword", Resources.Account.PasswordIncorrect);
                }
            }

            var profile = Mapper.Map<ProfileForm>(AuthenticationHelper.CurrentUser);
            profile.PasswordForm = model;

            return View(MVC.Profile.Views.Edit, profile);
        }


        [AuthorizeSignedUpUser]
        public virtual ActionResult ConfirmEmail(string activationKey)
        {
            try
            {
                var user = Repository.User.ConfirmNewEmail(activationKey);

                AuthenticationHelper.Authenticate(user.Email);

                Flash.Success(Resources.Account.EmailConfirmationSuccessful);
                return RedirectToAction(MVC.Profile.Edit());
            } 
            catch (UserNotFoundException)
            {
                Flash.Error(Resources.Account.EmailConfirmationUserNotFound);
                return RedirectToAction(MVC.StaticPages.Home());
            }
        }

        [AuthorizeRoles(Role.Expert)]
        [HttpPost]
        public virtual ActionResult EditExpertPublicName([Bind(Prefix = "ExpertProfileFormModel.ExpertProfileForm.PublicNameForm")] PublicNameForm model)
        {
            if (!ModelState.IsValid)
            {
                var profile = Mapper.Map<ProfileForm>(AuthenticationHelper.CurrentUser);
                profile.ExpertProfileFormModel.ExpertProfileForm.PublicNameForm = model;
                return View(MVC.Profile.Views.Edit, profile);
            }

            var expert = AuthenticationHelper.CurrentUser.Expert;
            expert.VerificationStatus = ExpertVerificationStatus.ToReverify;
            expert.PublicName = model.PublicName;

            Log.ExpertQualificationChangedEvent(expert.User, Resources.Account.PublicNameChanged);

            Repository.Expert.Update(expert);

            Flash.Success(Resources.Account.PublicNameChangedForVerified);
            return RedirectToAction(MVC.Profile.Edit());
        }

        [AuthorizeRoles(Role.Moderator)]
        [HttpPost]
        public virtual ActionResult EditModeratorPublicName([Bind(Prefix = "ModeratorPublicNameForm")] PublicNameForm model)
        {
            if (!ModelState.IsValid)
            {
                var profile = Mapper.Map<ProfileForm>(AuthenticationHelper.CurrentUser);
                profile.ModeratorPublicNameForm = model;
                return View(MVC.Profile.Views.Edit, profile);
            }

            var moderator = AuthenticationHelper.CurrentUser.Moderator;
            moderator.PublicName = model.PublicName;

            Repository.Moderator.Update(moderator);

            Flash.Success(Resources.Account.PublicNameChanged);
            return RedirectToAction(MVC.Profile.Edit());
        }

        [AuthorizeRoles(Role.Expert)]
        [HttpPost]
        public virtual ActionResult EditPhoneNumber([Bind(Prefix = "ExpertProfileFormModel.ExpertProfileForm.PhoneNumberForm")] PhoneNumberForm form)
        {
            if (!ModelState.IsValid)
            {
                var profile = Mapper.Map<ProfileForm>(AuthenticationHelper.CurrentUser);
                profile.ExpertProfileFormModel.ExpertProfileForm.PhoneNumberForm = form;
                return View(MVC.Profile.Views.Edit, profile);
            }

            var expert = AuthenticationHelper.CurrentUser.Expert;
            expert.PhoneNumber = form.PhoneNumber;
            expert.VerificationStatus = ExpertVerificationStatus.ToReverify;

            Repository.Expert.Update(expert);

            Log.ExpertQualificationChangedEvent(expert.User, Resources.Account.PhoneNumberChanged);

            Flash.Success(Resources.Account.PhoneNumberChangedToVerify);
            return RedirectToAction(MVC.Profile.Edit());
        }

        [AuthorizeRoles(Role.Expert)]
        [HttpPost]
        public virtual ActionResult EditCategories([Bind(Prefix = "ExpertProfileFormModel.ExpertProfileForm.ExpertProfileCategoriesForm")] ExpertProfileCategoriesForm model)
        {
            if (model == null)
            {
                ModelState.AddModelError("ExpertProfileFormModel.ExpertProfileForm.ExpertProfileCategoriesForm", Resources.Account.SelectCategory);
                model = new ExpertProfileCategoriesForm {SelectedCategories = new List<int>()};

                var profile = Mapper.Map<ProfileForm>(AuthenticationHelper.CurrentUser);
                profile.ExpertProfileFormModel.ExpertProfileForm.ExpertProfileCategoriesForm = model;
                return View(MVC.Profile.Views.Edit, profile);
            }
            var flashMessage = Resources.Account.CategoriesUpdated;

            var user = AuthenticationHelper.CurrentUser;

            var selectedCategories = Repository.Category.Find(query: c => c.ByIds(model.SelectedCategories));
            
            //if any new category, expert becomes not verified
            if (selectedCategories.Any(newCategory => !user.Expert.Categories.Contains(newCategory)))
            {
                Log.ExpertQualificationChangedEvent(user, Resources.Account.CategoriesUpdated);
                user.Expert.VerificationStatus = ExpertVerificationStatus.ToReverify;
                if (user.Expert.IsVerified)
                    flashMessage = Resources.Account.CategoriesUpdatedForVerified;
            }

            user.Expert.Categories.Clear();
            user.Expert.Categories.AddRange(selectedCategories.ToList());

            Repository.User.Update(user);

            Flash.Success(flashMessage);
            return RedirectToAction(MVC.Account.ExpertCategoryAttributes());
        }

        [AuthorizeRoles(Role.Expert)]
        [HttpPost]
        public virtual ActionResult EditExpertMicroprofile([Bind(Prefix = "ExpertProfileFormModel.ExpertProfileForm.ExpertMicroprofileForm")] ExpertMicroprofileForm model)
        {
            if (!ModelState.IsValid)
            {
                var profile = Mapper.Map<ProfileForm>(AuthenticationHelper.CurrentUser);
                profile.ExpertProfileFormModel.ExpertProfileForm.ExpertMicroprofileForm = model;
                return View(MVC.Profile.Views.Edit, profile);
            }

            var user = AuthenticationHelper.CurrentUser;

            user.Expert.Microprofiles.First().Description = model.Description;
            user.Expert.Microprofiles.First().Position = model.Position;
            user.Expert.VerificationStatus = ExpertVerificationStatus.ToReverify;

            Repository.User.Update(user);

            Log.ExpertQualificationChangedEvent(user, Resources.Account.MicroprofileUpdated);

            Flash.Success(Resources.Account.MicroprofileUpdatedForVerified);
            return RedirectToAction(MVC.Profile.Edit());
        }

        [AuthorizeSignedUpUser]
        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult EditMailConfiguration([Bind(Prefix = "MailConfigurationForm")] MailConfigurationForm model)
        {
            var user = AuthenticationHelper.CurrentUser;

            var emailTypes = EmailMetadataHelper.GetTypesForRecipientType(model.RecipientTypes);
            foreach (var emailType in emailTypes)
            {
                var isSet = model.SelectedEmails[emailType];
                if (isSet)
                    user.EmailConfiguration |= emailType;
                else
                    user.EmailConfiguration &= ~emailType;                 
            }

            Repository.User.Update(user);

            Flash.Info(Resources.Account.MailCfgUpdated);
            return RedirectToAction(MVC.Profile.Edit());
        }

        [AuthorizeRoles(Role.Expert)]
        [HttpPost]
        public virtual ActionResult UploadImage()
        {
            var expert = AuthenticationHelper.CurrentUser.Expert;
            foreach (string inputTagName in Request.Files)
            {
                var file = Request.Files[inputTagName];
                if (UploadHelper.ValidateUpload(file.ContentLength, file.ContentType, UploadHelper.FileType.Avatar))
                {
                    string filePath = Path.Combine(HttpContext.Server.MapPath("\\ProfileImages"), Path.GetFileName(expert.Id.ToString()));

                    string pathToDefaultFile = filePath + ".jpg";

                    try { file.SaveAs(pathToDefaultFile); }
                    catch(Exception ex){}

                    ImageHelper.SaveCroppedAndResizedCentered(pathToDefaultFile, 80, 80, filePath + "_80x80.jpg");
                    ImageHelper.SaveCroppedAndResizedCentered(pathToDefaultFile, 300, 300, filePath + "_300x300.jpg");

                    expert.HasPicture = true;
                    Repository.Expert.Update(expert);

                    Log.ExpertQualificationChangedEvent(expert.User, Resources.Account.ProfileImageChanged);
                }
                else
                {
                    Flash.Error(Resources.Attachments.AvatarSizeError);
                }

                return RedirectToAction(MVC.Profile.Edit());
            }

            return RedirectToAction(MVC.Profile.Edit());
        }

        [AssignMetadata]
        [AuthorizeRoles(Role.Expert)]
        public virtual ActionResult FeedbackList(int userId)
        {
            var user = AuthenticationHelper.CurrentUser;

            var model = Repository.Expert.GetExpertViewModel(user.Expert.Id);

            if (userId == user.Id) { model.IsCommentEnabled = true; }

            return View(model);
        }

        [AuthorizeRoles(Role.Expert)]
        [DefaultRouting]
        public virtual ActionResult FeedbackComment()
        {
            return PartialView(new FeedbackCommentForm());
        }

        [HttpPost]
        [DefaultRouting]
        [AuthorizeRoles(Role.Expert)]
        public virtual ActionResult FeedbackCommentForm(FeedbackCommentForm form)
        {
            var feedback = Repository.Expert.FindFeedback(form.FeedbackId);
            if (feedback.Thread.Expert != AuthenticationHelper.CurrentUser.Expert)
            {
                FormsAuthentication.RedirectToLoginPage();
                return null;
            }

            Repository.Thread.AddFeedbackComment(feedback, form.Comment);
            Log.Event<ExpertCommentedFeedbackEvent>(feedback.Thread, feedback.Thread.Expert.PublicName);
            Email.Send<FeedbackCommentEmail>(feedback.Thread);

            return RedirectToAction(MVC.Profile.FeedbackList(AuthenticationHelper.CurrentUser.Expert.Id));
        }

        [AssignMetadata]
        public virtual ActionResult ExpertOverview(int expertId, int? categoryId = null)
        {
            var expert = Repository.Expert.Get(expertId);

            var model = new ExpertOverviewQuestion(expert.Categories.OrderBy(c => c.Order))
                            {ExpertOverviewViewModel = Repository.Expert.GetExpertViewModel(expertId)};

            model.ThreadForm.DirectQuestionExpertId = expertId;
            model.ThreadForm.CategoryId = categoryId;

            if (AuthenticationHelper.IsExpert && expertId == AuthenticationHelper.CurrentUser.Expert.Id)
                model.ExpertOverviewViewModel.IsCommentEnabled = true;

            model.ExpertEvents = Repository.Event.GetExpertOverviewEvents(expertId);
            
            return View(model);
        }

        [AssignMetadata]
        public virtual ActionResult ExpertStatistics(int expertId)
        {
            var model = Repository.Expert.GetExpertViewModel(expertId);
            if (AuthenticationHelper.IsExpert && expertId == AuthenticationHelper.CurrentUser.Expert.Id)
                model.IsCommentEnabled = true;

            return View(model);
        }

        [AssignMetadata]
        [Authorize]
        public virtual ActionResult PartnerStatistics()
        {
            var model = Repository.User.GetPaymentStatistics(AuthenticationHelper.CurrentUser.Id);
            return View(model);
        }

        [DefaultRouting]
        public virtual ActionResult FeedbackListPartial()
        {
            return PartialView();
        }

        #region dodawanie opinii - not used

        [Authorize]
        public virtual ActionResult UserOpinion()
        {
            return PartialView(new OpinionForm());
        }

        [HttpPost]
        [Authorize]
        public virtual ActionResult UserOpinion(OpinionForm model)
        {
            if (ModelState.IsValid)
            {
                /*var opinion = ;
                opinion.OpinionContent = model.OpinionContent;
                opinion.OpinionMark = model.OpinionMark;
                opinion.AddressCity = model.AddressCity;
                Repository.UserOpinion.Add(opinion);*/
                    
                return RedirectToAction(MVC.StaticPages.Home());//thx for ur opinion
            }
            return RedirectToAction(MVC.StaticPages.Home());
        }

        #endregion dodawanie opinii end

        [DefaultRouting]
        public virtual ActionResult Opinions(int? categoryId = null)
        {
            var opinions = Repository.Feedback.Find(query: f => f.ByCategory(categoryId), order: f => f.CreationDate, itemsPerPage: 32);

            var random = new Random();
            var randomOpinions = opinions.OrderBy(o => random.Next()).ToList().Take(3);

            var model = new OpinionListModel
                            {
                                Opinions = randomOpinions
                            };

            return PartialView(model);
        }

        public virtual ActionResult OpinionSingle(Feedback model)
        {
            return PartialView(model);
        }

        [AssignMetadata]
        [AuthorizeSignedUpUser]
        public virtual ActionResult Balance([QueryParameter]GridSortOptions sortOptions = null, [QueryParameter]int? page = null)
        {
            var model = new BalanceListModel()
            {
                GridModel = GetBalanceListModel(t => t.ByOwner(AuthenticationHelper.CurrentUser).ByPendingStatus(false), sortOptions, page),
                AvailableBalance = AuthenticationHelper.CurrentUser.GetAvailableCash(),
                TotalBalance = AuthenticationHelper.CurrentUser.GetTotalCash(),
            };

            return View(model);
        }

        [AuthorizeRoles(Role.Expert | Role.Partner)]
        [DefaultRouting]
        public virtual ActionResult PayoffForm(PayoffForm form = null)
        {
            return PartialView(form ?? new PayoffForm
            {
                PayoffValue = AuthenticationHelper.CurrentUser.GetAvailableCash(),
                BankAccount = AuthenticationHelper.CurrentUser.BankAccountNumber
            });
        }

        [AuthorizeRoles(Role.Expert | Role.Partner)]
        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult Payoff(PayoffForm form)
        {
            var user = AuthenticationHelper.CurrentUser;

            if (!ModelState.IsValid)
                return PartialView(MVC.Profile.Views.PayoffForm, form);

            if (form.PayoffValue == decimal.Zero) return null;

            var availableCash = user.GetAvailableCash();
            if (availableCash < form.PayoffValue)
            {
                ModelState.AddModelError("PayoffValue",string.Format(Resources.Account.PayoffValueToHigh,availableCash));
                form.PayoffValue = availableCash;
                return PartialView(MVC.Profile.Views.PayoffForm,form);
            }

            var transfer = new Transfer
                               {
                                   Value = -form.PayoffValue.Value,
                                   Title = Resources.Payment.TransferCashPayoff,
                                   OrderDate = DateTime.Now,
                                   TransferDate = null,
                                   Payment = null,
                                   IsPending = false,
                                   Comment = null
                               };
            
            //AuthenticationHelper.CurrentUser.Transfers.Add(Transfer.Cash(-form.PayoffValue.Value, Resources.Payment.TransferCashPayoff, isTransfered: false));
            //AuthenticationHelper.CurrentUser.BankAccountNumber = form.BankAccount;
            user.Transfers.Add(transfer);
            user.BankAccountNumber = form.BankAccount;

            Repository.User.Update(AuthenticationHelper.CurrentUser);

            Email.Send<UserPayoffConfirmationEmail>(user);

            Log.Event<CashPaymentEvent>(user,additionalId:transfer.Id);

            Flash.Success(string.Format(Resources.Payment.FlashPayoffOrderSuccess, form.PayoffValue,form.BankAccount));

            return null;
        }

        [AssignMetadata]
        [Authorize]
        public virtual ActionResult PartnerProgram()
        {
            return View();
        }

        [AuthorizeRoles(Role.Expert)]
        [DefaultRouting]
        public virtual ActionResult ExpertWidgetGenerated()
        {
            Log.Event<ExpertWidgetGeneratedEvent>(AuthenticationHelper.CurrentUser);
            return null;
        }

        [DefaultRouting]
        public virtual ActionResult ProfileNavtabs(string navTitle)
        {
            return PartialView(MVC.Profile.Views._ProfileNavtabs, navTitle);
        }

        [DefaultRouting]
        [OutputCache(VaryByParam = "expertId", Duration = 3600)]
        public virtual ActionResult ExpertWidget(int expertId)
        {
            var model = Repository.Expert.GetExpertViewModel(expertId);
            return PartialView(model);
        }

        [DefaultRouting]
        [Authorize]
        public virtual ActionResult PartnerWidgetPreview()
        {
            return PartialView();
        }

        private SortableGridModel<Transfer> GetBalanceListModel(Func<IQueryable<Transfer>, IQueryable<Transfer>> query,
                                                               GridSortOptions sortOptions, int? page)
        {
            if (sortOptions == null)
                sortOptions = new GridSortOptions();

            if (sortOptions.Column == null)
            {
                sortOptions.Column = GridHelper.BalanceOrder.OrderDate;
                sortOptions.Direction = SortDirection.Descending;
            }

            var transferList = Repository.Transfer.Find(PagerHelper.PageSize, page ?? 1, query,
                                                        GridHelper.BalanceOrder.ColumnOrders[sortOptions.Column],
                                                        sortOptions.Direction == SortDirection.Ascending);

            var transferCount = Repository.Transfer.Count(query);

            return new SortableGridModel<Transfer>
            {
                Data = transferList,
                SortOptions = sortOptions,
                Pagination = PagerHelper.Pagination(page, transferCount)
            };
        }

        [DefaultRouting]
        public virtual ActionResult RecommendExpert(int? recommenderId = null)
        {
            var categories =
                Repository.Category.All().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

            if(recommenderId == null)
                return PartialView(new RecommendExpertForm { Categories = categories, RecommendedExpertComment = Resources.Account.RecommendationEmailMessageTemplate});

            var recommender = Repository.User.Get(recommenderId ?? 0);

            return PartialView(new RecommendExpertForm{RecommenderId = recommender.Id,
                RecommenderEmail = recommender.Email, 
                RecommenderName = recommender.IsExpert ? recommender.Expert.FirstName : null,
                RecommenderSurname = recommender.IsExpert ? recommender.Expert.LastName : null,
                RecommendedExpertComment = Resources.Account.RecommendationEmailMessageTemplate,
                Categories = categories
            });
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult RecommendExpertForm(RecommendExpertForm form)
        {
            if (!ModelState.IsValid)
            {
                form.Categories = Repository.Category.All().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
                return PartialView(MVC.Profile.Views.RecommendExpert, form);
            }

            var recommendation = new Recommendation
            {
                RecommenderName = form.RecommenderName,
                RecommenderSurname = form.RecommenderSurname,
                RecommenderEmail = form.RecommenderEmail,
                RecommendedExpertComment = form.RecommendedExpertComment,
                RecommendedExpertEmail = form.RecommendedExpertEmail,
                RecommendedExpertName = form.RecommendedExpertName,
                RecommendedExpertSurname = form.RecommendedExpertSurname,
                RecommendedCategory = Repository.Category.Get(form.RecommendedExpertCategoryId),
            };

            if(form.RecommenderId != null)
            {
                var recommender = Repository.User.Get(form.RecommenderId ?? 0);
                recommendation.Recommender = recommender;
            }

            recommendation.GenerateRecommendationKey();

            Repository.Recommendation.Add(recommendation);
            Repository.Recommendation.Update(recommendation);

            Email.Send<RecommendationEmail>(recommendation);

            Flash.Success(Resources.Account.RecommendationFlash, true, form.RecommenderId);

            return null;
        }

        public virtual ActionResult CheckExpertRegistration(string recommendationKey)
        {
            FormsAuthentication.SignOut();

            try
            {
                Repository.Recommendation.CheckRecommendationKey(recommendationKey);

                Flash.Success(Resources.Account.RecommendationChecked);
                return RedirectToAction(MVC.Account.ExpertSignUp(recommendationKey));
            }
            catch (RecommendationNotFoundException)
            {
                Flash.Error(Resources.Account.RecommendationNotFound);
                return RedirectToAction(MVC.StaticPages.Home());
            }
            catch (ExpertAlreadyRegisteredException)
            {
                Flash.Warning(Resources.Account.RecommendationAlreadyUsed);
                return RedirectToAction(MVC.StaticPages.Home());
            }
        }

        [AssignMetadata]
        [Authorize]
        public virtual ActionResult MyQuestions(int? page = null)
        {
            var currentThreads = Repository.Thread.Find(query: t => t.ByAuthor(AuthenticationHelper.CurrentUser).ByState(ThreadState.Unoccupied,ThreadState.Occupied,ThreadState.Reserved)).OrderByDescending(d => d.CreationDate).ToList();

            var allAcceptedThreads = Repository.Thread.Find(query: t => t.ByAuthor(AuthenticationHelper.CurrentUser).ByState(ThreadState.Accepted, ThreadState.Closed)).OrderByDescending(d => d.CreationDate).ToList();
            var accceptedThreads = allAcceptedThreads.Skip(page == null ? 0 : (int)(page - 1) * PagerHelper.PageSize).Take(PagerHelper.PageSize).ToList();
           

            var model = new MyQuestions{
                CurrentThreads = currentThreads,
                AcceptedThreads = accceptedThreads
            };

            if (Request.IsAjaxRequest())
                return PartialView(MVC.Thread.Views._ProfileQuestionsItems, model.AcceptedThreads);

            return View(model);
        }

        [AssignMetadata]
        [AuthorizeRoles(Role.Expert)]
        public virtual ActionResult MyAnswers(int? page = null)
        {
            var allThreads = Repository.Thread.Find(query: t => t.ByExpert(AuthenticationHelper.CurrentUser.Expert).ByPaymentStatus(true)).OrderByDescending(d => d.CreationDate).ToList();
            var threads = allThreads.Skip(page == null ? 0 : (int)(page-1) * PagerHelper.PageSize).Take(PagerHelper.PageSize).ToList();

            if (Request.IsAjaxRequest())
                return PartialView(MVC.Thread.Views._ProfileQuestionsItems, threads);

            return View(threads);
        }
    }
}
