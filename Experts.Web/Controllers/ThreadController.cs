using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using Experts.Core.Entities;
using Experts.Core.Entities.Events;
using Experts.Core.Exceptions;
using Experts.Core.Repositories;
using Experts.Core.Utils;
using Experts.Core.ViewModels;
using Experts.Web.Filters;
using Experts.Web.Models.Forms;
using Experts.Web.Models.Shared;
using Experts.Web.Models.StaticPages;
using Experts.Web.Models.Threads;
using Experts.Web.Utils;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid;
using Experts.Web.Helpers;
using System.IO;

namespace Experts.Web.Controllers
{
    public partial class ThreadController : BaseController
    {
        [AssignMetadata]
        [HttpPost]
        public virtual ActionResult CategoryAttributes([Bind(Prefix = "ThreadForm")] ThreadForm form, int? brokerId)
        {
            if (brokerId != null)
                BrokerHelper.SetBrokerCookie(brokerId.Value);

            if (!ModelState.IsValid)
            {
                Log.Info(GetType(), "form-action:Form invalid");
                if (!ModelState.IsValidField("ThreadForm.CategoryId"))
                    Flash.Error("Wybierz kategorię pytania");

                return View(MVC.StaticPages.Views.Home,
                            new HomeModel(Repository.Category.All()) {ThreadForm = form});
            }

            Log.Info(GetType(), "form-action:Asked question '{0}'", form.Content);

            var categoryAttributes = Repository.Category.Get(form.CategoryId.Value).Attributes;
            var model = new CategoryAttributesThreadFormModel { ThreadForm = form, CategoryAttributes = categoryAttributes };

            if (form.SeoKeywordId.HasValue)
            {
                var categoryAttributeValues = Repository.SEOKeyword.Get(form.SeoKeywordId.Value).CategoryAttributes;
                model.CategoryAttributeValues = CategoryAttributeHelper.GetCategoryAttributeValueModel(categoryAttributeValues);
            }

            if (categoryAttributes.Count == 0)
            {
                PrepareOptions(model.ThreadForm);
                if(!form.DirectQuestionExpertId.HasValue)
                {
                    form.InterestedExpert = GetBestExpertId(form);
                }
                return View(MVC.Thread.Views.Options, model.ThreadForm);
            }

            if (form.TemporaryAttachmentFolder == null)
                form.GenerateTemporaryAttachmentFolder();

            return View(model);
        }

        [DefaultRouting]
        [HttpPost]
        public virtual ActionResult ChildCategoryAttributes([Bind(Prefix = "ThreadForm.AttributeValues")]  AttributeValueModel[] attributeValues, int attributeId)
        {
            var helper = new CategoryAttributeHelper();
            var categoryAttributeValues = helper.GetCategoryAttributeValues(attributeValues != null ? attributeValues.Where(cv => cv.AttributeId == attributeId) : new AttributeValueModel[0]);
            var allSubAttributes = categoryAttributeValues.SelectMany(cav => cav.Attribute.ChildAttributes);
            var selectedSubAttributes = allSubAttributes.Where(sa => categoryAttributeValues.Any(cav => cav.SelectedOptions.Intersect(sa.ParentOptions).Any()));

            var model = new CategoryAttributesThreadFormModel
                            {
                                CategoryAttributes = selectedSubAttributes.ToList(),
                                CategoryAttributeValues = attributeValues
                            };

            return PartialView(MVC.Thread.Views._CategoryAttributes, model);
        }

        [AssignMetadata]
        [HttpPost]
        public virtual ActionResult Options([Bind(Prefix = "ThreadForm")] ThreadForm model)
        {
            model.InterestedExpert = GetBestExpertId(model);

            PrepareOptions(model);
            return View(model);
        }

        [AssignMetadata]
        public virtual ActionResult Options()
        {
            var model = ThreadMemoryHelper.PopRememberedThread();
            model.InterestedExpert = GetBestExpertId(model);

            SetValue(model);

            return View(model);
        }

        [AssignMetadata]
        public virtual ActionResult CategoryAttributes()
        {
            var model = ThreadMemoryHelper.PopRememberedThread();
            SetValue(model);

            return View(MVC.Thread.Views.Options, model);
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult GetThreadValue(ThreadForm model)
        {
            SetValue(model);
            return Json(model.Value.ToString("G"));
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult GetThreadVerbocity(ThreadForm model)
        {
            return Json(model.Verbosity.ToString());
        }

        [AssignMetadata]
        [HttpPost]
        public virtual ActionResult Save(ThreadForm form)
        {
            ThreadMemoryHelper.PopRememberedThread();

            if (!ModelState.IsValid)
            {
                
                Log.Info(GetType(),"Thread Save Validation Failed with errors: {0}",string.Join(";",ModelState.SelectMany(keyValuePair => keyValuePair.Value.Errors).Select(x => x.ErrorMessage)));
                return View(MVC.Thread.Views.Options, form);
            }

            var thread = Mapper.Map<Thread>(form);

            var helper = new CategoryAttributeHelper();
            var categoryAttributes = helper.GetCategoryAttributeValues(form.AttributeValues).ToList();
            foreach (var categoryAttributeValue in categoryAttributes)
                thread.CategoryAttributes.Add(categoryAttributeValue);

            thread.Author = AuthenticationHelper.CurrentUser;
            thread.Category = Repository.Category.Get(form.CategoryId.Value);

            if (form.CustomValue == null)
                thread.Value = Repository.Category.GetValue(form.CategoryId.Value, ThreadPriority.Medium, form.Verbosity);

            thread.Posts.First().Author = AuthenticationHelper.CurrentUser;
            thread.Posts.First().Type = PostType.Question;

            var brokerId = BrokerHelper.GetBrokerIdFromCookie();
            if (brokerId != null)
                thread.Broker = Repository.User.Get(brokerId.Value);

            Repository.Thread.Add(thread);
            
            if (form.DirectQuestionExpertId != null) { DirectQuestion(form.DirectQuestionExpertId.Value, thread.Id); }

            if (form.CustomValue != null) { EventLog.Event<UserDefinedPriceEvent>(thread); }

            form.PaymentForm.RelatedId = thread.Id;

            var attachmentsDirPath = HttpContext.Server.MapPath("~/Attachments/");
            if (Directory.Exists(attachmentsDirPath + form.TemporaryAttachmentFolder)) { MoveAttachmentsFromTemporaryFolder(form.TemporaryAttachmentFolder, thread.Id); }

            var provisionData = "\r\n" + "Prowizja eksperta: " + thread.ExpertProvision.ToString() + 
                "\r\n" + "Prowizja brokera: " + thread.BrokerProvision.ToString();
            EventLog.Event<NewThreadEvent>(thread, provisionData);

            var model = form.PaymentForm.PreparePayment(Url);

            if (model.ImmediateRedirectActionResult != null)
                return RedirectToAction(model.ImmediateRedirectActionResult);

            return View(MVC.Payment.Views.PaymentRedirect, model);
        }

        [AssignMetadata]
        [AuthorizeRoles(Role.Expert)]
        public virtual ActionResult AvailableQuestionList(int? categoryId = null, [QueryParameter]string sortColumn = null, [QueryParameter]int? page = null)
        {
            if (sortColumn == null)
                sortColumn = GridHelper.ThreadOrder.Match;

            var expertCategories = AuthenticationHelper.CurrentUser.Expert.Categories;

            var showInner = AuthenticationHelper.CurrentUser.Expert.IsInner;

            Func<ThreadWithMatch, IComparable> order;
            if (sortColumn == GridHelper.ThreadOrder.Match)
                order = t => t.Match;
            else
                order = t => GridHelper.ThreadOrder.ColumnOrders[sortColumn](t.Thread);

            var results = Repository.Thread.FindThreadsWithMatches(AuthenticationHelper.CurrentUser.Expert,
                                    additionalQuery: t => t.ByCategories(expertCategories)
                                                            .WithoutAuthor(AuthenticationHelper.CurrentUser)
                                                            .ByPaymentStatus(true)
                                                            .ByInnerStatus(showInner)
                                                            .ByState(ThreadState.Unoccupied, ThreadState.Occupied),
                                    itemsPerPage: PagerHelper.PageSize,
                                    page: page ?? 1,
                                    order: order,
                                    ascending: false);


            var threads = results.Select(t => t.Thread).ToList();

            if(Request.IsAjaxRequest())
                return PartialView(MVC.Thread.Views._QuestionsListItems, threads);

            return View(threads);
        }


        //private SortableGridModel<ThreadWithMatch> GetAvailableQuestionListModel(Func<IQueryable<Thread>, IQueryable<Thread>> query, GridSortOptions sortOptions, int? page)
        //{
        //    sortOptions = InitGridSortOptions(sortOptions);

        //    Func<ThreadWithMatch, IComparable> order;
        //    if (sortOptions.Column == GridHelper.ThreadOrder.Match)
        //        order = t => t.Match;
        //    else
        //        order = t => GridHelper.ThreadOrder.ColumnOrders[sortOptions.Column](t.Thread);

        //    var threadList = Repository.Thread.FindThreadsWithMatches(AuthenticationHelper.CurrentUser.Expert,
        //                                                              additionalQuery: query,
        //                                                              order: order,
        //                                                              ascending: sortOptions.Direction == SortDirection.Ascending,
        //                                                              itemsPerPage: PagerHelper.PageSize,
        //                                                              page: page ?? 1).ToList();

        //    ///// REMOVE --- TO TEST - BEGIN

        //    for(var i = 0; i < 15; i++)
        //    {
        //        threadList.Add(threadList[0]);
        //    }

        //    //// REMOVE ---- TO TEST - END

        //    var threadListCount = Repository.Thread.Count(query);

        //    return new SortableGridModel<ThreadWithMatch>
        //    {
        //        Data = threadList, 
        //        SortOptions = sortOptions,
        //        Pagination = PagerHelper.Pagination(page, threadListCount)
        //    };
        //}

        //public virtual ActionResult CatalogQuestionList(int? categoryId = null, [QueryParameter]GridSortOptions sortOptions = null, [QueryParameter]int? page = null)
        //{
        //    return View(GetCategoryQuestionListModel(categoryId, sortOptions, page, ThreadSanitizationStatus.Sanitized));
        //}

        [AssignMetadata]
        public virtual ActionResult CatalogQuestionList(int? page = null)
        {
            var threads = Repository.Thread.Find(PagerHelper.PageSize, page ?? 1, t => t.BySanitizationStatus(ThreadSanitizationStatus.Sanitized)).ToList();

            if(Request.IsAjaxRequest())
                return PartialView(MVC.Thread.Views._QuestionsListItems, threads);

            return View(threads);
        }

        [AssignMetadata]
        [AuthorizeRoles(Role.Moderator)]
        public virtual ActionResult SanitizationQuestionList(ThreadSanitizationStatus? sanitizationStatus = null, [QueryParameter]GridSortOptions sortOptions = null, [QueryParameter]int? page = null)
        {
            var questionListModel = GetQuestionListModel(t => t.BySanitizationStatus(sanitizationStatus ?? ThreadSanitizationStatus.NotSanitized).ByState(ThreadState.Closed), sortOptions, page);
            var model = new SanitizationQuestionListModel { GridModel = questionListModel, SelectedStatus = sanitizationStatus ?? ThreadSanitizationStatus.NotSanitized };

            return View(model);
        }

        [AssignMetadata]
        [AuthorizeRoles(Role.Moderator)]
        public virtual ActionResult ModeratorQuestionList(int? categoryId = null, [QueryParameter]GridSortOptions sortOptions = null, [QueryParameter]int? page = null)
        {
            return View(GetCategoryQuestionListModel(categoryId, sortOptions, page));
        }

        private QuestionListModel<Thread> GetCategoryQuestionListModel(int? categoryId = null, [QueryParameter]GridSortOptions sortOptions = null, [QueryParameter]int? page = null, ThreadSanitizationStatus? state = null)
        {
            var availableCategories = Repository.Category.All();
            var filterCategories = availableCategories;

            if (categoryId > 0 && availableCategories.Any(c => c.Id == categoryId))
            {
                var selectedCategory = availableCategories.Single(c => c.Id == categoryId);
                filterCategories = new List<Category> { selectedCategory };
            }

            var questionListModel = GetQuestionListModel(t => t.ByCategories(filterCategories).BySanitizationStatus(state), sortOptions, page);

            return new QuestionListModel<Thread>(availableCategories)
                {
                    GridModel = questionListModel,
                    SelectedCategory = categoryId != null ? availableCategories.Single(c => c.Id == categoryId) : null
                };
        }

        private SortableGridModel<Thread> GetQuestionListModel(Func<IQueryable<Thread>, IQueryable<Thread>> query,
                                                               GridSortOptions sortOptions, int? page)
        {
            sortOptions = InitGridSortOptions(sortOptions);

            var threadList = Repository.Thread.Find(PagerHelper.PageSize, page ?? 1, query,
                                                    GridHelper.ThreadOrder.ColumnOrders[sortOptions.Column],
                                                    sortOptions.Direction == SortDirection.Ascending);

            var threadListCount = Repository.Thread.Count(query);

            return new SortableGridModel<Thread>
                       {
                           Data = threadList,
                           SortOptions = sortOptions,
                           Pagination = PagerHelper.Pagination(page, threadListCount)
                       };
        }



        private GridSortOptions InitGridSortOptions(GridSortOptions sortOptions)
        {
            if (sortOptions == null)
                sortOptions = new GridSortOptions();

            if (sortOptions.Column == null)
            {
                sortOptions.Column = GridHelper.ThreadOrder.CreationDate;
                sortOptions.Direction = SortDirection.Descending;
            }

            return sortOptions;
        }

        private void SetValue(ThreadForm model)
        {
            var baseValue = Repository.Category.GetValue(model.CategoryId.Value, ThreadPriority.Medium, model.Verbosity);

            var categoryAttributes = new CategoryAttributeHelper().GetCategoryAttributeValues(model.AttributeValues).ToList();
            var priceModifier = 1 + categoryAttributes.Sum(ca => ca.SelectedOptions.Sum(o => o.PriceModifier));

            model.Value = Math.Round(baseValue*priceModifier, 0);
        }

        [AssignMetadata]
        [Authorize]
        public virtual ActionResult ThreadDetails(int threadId, PostForm postForm = null)
        {
            var thread = Repository.Thread.Get(threadId);

            if (!IsAuthorExpertOrModerator(thread))
            {
                FormsAuthentication.RedirectToLoginPage();
                return null;
            }

            if(thread.State == ThreadState.Hidden)
            {
                Flash.Error(Resources.Thread.ThreadHidden);
                return RedirectToAction(MVC.StaticPages.Home());
            }

            if (!thread.IsPaid && !IsAuthor(thread) && !AuthenticationHelper.IsModerator)
            {
                FormsAuthentication.RedirectToLoginPage();
                return null;
            }

            if(AuthenticationHelper.CurrentUser.IsExpert)
            {
                if (AuthenticationHelper.CurrentUser != thread.Author && AuthenticationHelper.CurrentUser.Expert.VerificationStatus != ExpertVerificationStatus.Verified)
                {
                    if(thread.Expert != AuthenticationHelper.CurrentUser.Expert)
                    {
                        Flash.Error(Resources.Account.VerificationNeeded);
                        return RedirectToAction(MVC.StaticPages.Home());   
                    }
                }
            }

            string view;
            IEnumerable<PostType> postTypes;
            var model = new ThreadDetailsModel(thread);

            if (AuthenticationHelper.IsExpert && thread.Author != AuthenticationHelper.CurrentUser)
            {
                postTypes = new List<PostType> { PostType.Answer, PostType.DetailsRequest };
                view = MVC.Thread.Views.ThreadDetailsForExperts;
            }
            else if (thread.Author == AuthenticationHelper.CurrentUser)
            {
                postTypes = new Collection<PostType> { PostType.Details };
                view = MVC.Thread.Views.ThreadDetailsForAuthor;
            }
            else // moderator
            {
                postTypes = new List<PostType> { PostType.ModeratorAnswer, PostType.ExpertPM };
                view = MVC.Thread.Views.ThreadDetailsForModerator;
                model.IsModerationMode = true;
                model.ThreadEvents = Repository.Event.GetThreadEvents(threadId);
            }

            model.PostFormModel = new PostFormModel(postTypes, model.Thread.Id);
            if (postForm != null && postForm.ThreadId != 0 && postForm.Type != 0)
                model.PostFormModel.PostForm = postForm;

            return View(view, model);
        }

        [AssignMetadata]
        public virtual ActionResult CatalogThreadDetails(int threadId, string title)
        {
            var thread = Repository.Thread.Get(threadId);
            if (thread.SanitizationStatus != ThreadSanitizationStatus.Sanitized)
                return HttpNotFound();

            var expert = thread.Expert;

            var model = new CatalogThreadQuestion(expert.Categories)
                {
                    Thread = thread,
                    ExpertOverviewViewModel = Repository.Expert.GetExpertViewModel(expert.Id)
                };

            model.ThreadForm.DirectQuestionExpertId = expert.Id;

            ViewBag.PageTitle = thread.CatalogThreadTitle;
            
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [DefaultRouting]
        [ValidateInput(false)]
        public virtual ActionResult UpdatePost(int postId, string postContent)
        {
            var post = Repository.Thread.GetPost(postId);

            if (!AuthenticationHelper.IsModerator && post.Author.Id != AuthenticationHelper.CurrentUser.Id)
            {
                FormsAuthentication.RedirectToLoginPage();
                return null;
            }

            post.Content = postContent;

            post.LastModificationDate = DateTime.Now;

            Repository.Thread.Update(post.Thread);

            return Content(post.LastModificationDate.ToTimeSinceFormat());
        }


        [ChildActionOnly]
        [DefaultRouting]
        public virtual ActionResult ShowPost(Post post, bool isCatalogMode = false, bool isSanitizationMode = false)
        {
            if (isSanitizationMode)
            {
                return PartialView(MVC.Thread.Views._PostSanitize, post);
            }

            if(isCatalogMode)
            {
                post.Content = post.GetPublicNameIfNotEmpty();
            }

            return PartialView(post.Type == PostType.Attachment ? MVC.Thread.Views._AttachmentPost : MVC.Thread.Views._Post, new PostViewModel {Post = post, IsCatalogMode = isCatalogMode});
        }

        [AssignMetadata]
        [AuthorizeRoles(Role.Moderator)]
        public virtual ActionResult Sanitization(int threadId)
        {
            var thread = Repository.Thread.Get(threadId);

            thread.CatalogSanitizedThreadTitle = thread.CatalogThreadTitle;

            var model = new ThreadSanitizationDetailsModel(thread) { IsSanitizationMode = true };

            return View(MVC.Thread.Views.ThreadDetailsForSanitization, model);
        }

        [AssignMetadata]
        [AuthorizeRoles(Role.Moderator)]
        [DefaultRouting]
        [HttpPost]
        public virtual ActionResult EditPublicSanitizedThreadTitle([Bind(Prefix = "Thread")] Thread model)
        {
            var thread = Repository.Thread.Get(model.Id);

            if(ModelState.IsValid)
            {
                thread.CatalogSanitizedThreadTitle = model.CatalogSanitizedThreadTitle;
                Repository.Thread.Update(thread);
                return RedirectToAction(MVC.Thread.Sanitization(thread.Id));
            }

            var modelError = new ThreadSanitizationDetailsModel(thread) { IsSanitizationMode = true };
            thread.CatalogSanitizedThreadTitle = model.CatalogSanitizedThreadTitle;

            return View(MVC.Thread.Views.ThreadDetailsForSanitization, modelError);
        }


        [AuthorizeRoles(Role.Moderator)]
        [DefaultRouting]
        [HttpPost]
        public virtual ActionResult SanitizationUpdatePost(int postId, string postPublicContent)
        {
            var post = Repository.Thread.GetPost(postId);
            post.PublicContent = postPublicContent;

            Repository.Thread.Update(post.Thread);

            return Content(postPublicContent);
        }

        [AuthorizeRoles(Role.Moderator)]
        [DefaultRouting]
        [HttpPost]
        public virtual ActionResult SanitizationUpdateThread(int threadId, ThreadSanitizationStatus sanitizationStatus)
        {
            var thread = Repository.Thread.Get(threadId);
            thread.SanitizationStatus = sanitizationStatus;

            Repository.Thread.Update(thread);

           return new EmptyResult();
        }

        [AuthorizeRoles(Role.Moderator)]
        [DefaultRouting]
        [HttpPost]
        public virtual ActionResult TogglePostVisibility(int postId)
        {
            var post = Repository.Thread.GetPost(postId);

            if (post.IsFirstInThread())
                throw new FirstPostCannotBeHiddenException();

            post.IsPubliclyVisible = !post.IsPubliclyVisible;

            var result = new { linkLabel = post.IsPubliclyVisible ? Resources.Thread.PostHide : Resources.Thread.PostShow, visibility = post.IsPubliclyVisible };

            Repository.Thread.Update(post.Thread);

            return Json(result);
        }

        public virtual ActionResult ShowExpertInfo(Thread thread)
        {
            return thread.Expert == null
                       ? MultiExpertBox(true, thread.Category.Id)
                       : SingleExpertBox(thread.Expert.Id, Resources.Thread.AnsweringExpert);
        }

        [DefaultRouting]
        public virtual ActionResult SingleExpertBox(int expertId, 
            [QueryParameter]string title = null, 
            [QueryParameter]int? categoryThreadId = null, 
            [QueryParameter]bool showVerificationDetails = true, 
            [QueryParameter]bool isEmbedded = false)
        {
            return PartialView(MVC.Thread.Views._ExpertBox, Repository.Expert.GetExpertViewModel(expertId, title, categoryThreadId, showVerificationDetails, isEmbedded));
        }

        public virtual ActionResult MultiExpertBox(bool feedbacksVisible, int? categoryId = null)
        {
            var expertIds = GetRandomExpertIds(categoryId);
            var initialExpertId = expertIds.First();

            var model = new MultiExpertBoxModel
                            {
                                InitialExpert = Repository.Expert.GetExpertViewModel(initialExpertId),
                                ExpertIds = expertIds,
                                AreFeedbacksVisible = feedbacksVisible
                            };
            model.InitialExpert.AreFeedbacksVisible = feedbacksVisible;

            if(categoryId != null)
            {
                model.Title = Resources.Thread.ExpertsFromCategory;
            }
            else
            {
                model.Title = Resources.StaticPages.HomeOurExperts;
            }

            return PartialView(MVC.Thread.Views._MultiExpertBox, model);
        }

        [DefaultRouting]
        public virtual ActionResult MultiExpertBoxData(int? categoryId = null)
        {
            return Json(GetRandomExpertIds(categoryId), JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<int> GetRandomExpertIds(int? categoryId)
        {
            IEnumerable<int> expertIds;
            if (categoryId.HasValue)
            {
                var category = Repository.Category.Get(categoryId.Value);
                expertIds = Repository.Expert.Find(query: e => e.ByCategory(category).ByPublic()).Select(e => e.Id);
            }
            else
            {
                expertIds = Repository.Expert.Find(query: e => e.ByPublic()).Select(e => e.Id);
            }

            var activeExpertsIds = ActiveUsersHelper.GetActivePublicExpertsIds(categoryId);
            var inactiveExpertsIds = expertIds.Except(activeExpertsIds);

            if (AuthenticationHelper.IsExpert)
            {
                var except = new[] {AuthenticationHelper.CurrentUser.Expert.Id};
                activeExpertsIds = activeExpertsIds.Except(except);
                inactiveExpertsIds = inactiveExpertsIds.Except(except);
            }

            var random = new Random();
            activeExpertsIds = activeExpertsIds.OrderBy(aei => random.Next()).ToList();
            inactiveExpertsIds = inactiveExpertsIds.OrderBy(aei => random.Next()).ToList();

            return activeExpertsIds.Concat(inactiveExpertsIds);
        }

        private int GetBestExpertId(ThreadForm form)
        {
            var categoryAttributes = new CategoryAttributeHelper().GetCategoryAttributeValues(form.AttributeValues).ToList();
            var experts = Repository.Expert.FindClosestMatches(form.CategoryId.Value, categoryAttributes, 10);

            var bestExpert = experts.First();
            var bestMatch = Repository.Expert.GetMatch(bestExpert, form.CategoryId.Value, categoryAttributes);

            var activeExpertsIds = ActiveUsersHelper.GetActivePublicExpertsIds(form.CategoryId);
            var bestActiveExpert = experts.FirstOrDefault(e => activeExpertsIds.Contains(e.Id));
            if (bestActiveExpert == null)
                return bestExpert.Id;

            var bestActiveMatch = Repository.Expert.GetMatch(bestActiveExpert, form.CategoryId.Value, categoryAttributes);

            return (double) bestActiveMatch/bestMatch > 0.8 ? bestActiveExpert.Id : bestExpert.Id;
        }

        [DefaultRouting]
        public virtual ActionResult ExpertInfo(int expertId, bool feedbacksVisible)
        {
            var model = Repository.Expert.GetExpertViewModel(expertId);
            model.AreFeedbacksVisible = feedbacksVisible;

            return PartialView(MVC.Thread.Views._ExpertOverview, model);
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult CreatePost([Bind(Prefix = "PostForm")] PostForm model)
        {
            if (!ModelState.IsValid)
                return ThreadDetails(model.ThreadId, model);

            var thread = Repository.Thread.Get(model.ThreadId);

            if(AuthenticationHelper.IsExpert && thread.Expert == null)
            {
                Flash.Error(Resources.Thread.OccupyTimeout);
                return RedirectToAction(MVC.Thread.ThreadDetails(thread.Id));
            }

            var isThreadExpert = AuthenticationHelper.IsExpert && AuthenticationHelper.CurrentUser == thread.Expert.User;
            if (AuthenticationHelper.CurrentUser != thread.Author && !isThreadExpert && !AuthenticationHelper.IsModerator)
            {
                FormsAuthentication.RedirectToLoginPage();
                return null;
            }

            var post = Mapper.Map<Post>(model);
            post.Author = AuthenticationHelper.CurrentUser;

            if ((thread.State == ThreadState.Unoccupied
                 || (thread.State == ThreadState.Occupied && thread.ExpertReleaseDate != null))
                && post.Author != thread.Author)
            {
                thread.Expert = post.Author.Expert;
                thread.State = ThreadState.Occupied;
                thread.ExpertReleaseDate = null;
                post.Author.Expert.Answers.Add(thread);

                var analyzingPost = thread.Posts.Single(p => p.Type == PostType.Analyzing);
                SystemPostFactory.ChangePostToAnswered(analyzingPost, thread.Expert);
            }

            Repository.Thread.Update(thread);
            Repository.Thread.AddPost(thread, post);

            if (post.Type == PostType.DetailsRequest)
            {
                Email.Send<ThreadMoreDetailsEmail>(thread);
                EventLog.Event<DetailsRequestEvent>(thread, AuthenticationHelper.CurrentUser.Expert.PublicName);
            }

            if (isThreadExpert && (post.Type == PostType.Answer))
            {
                Email.Send<ThreadAnsweredEmail>(thread);
                EventLog.Event<QuestionAnsweredEvent>(thread, AuthenticationHelper.CurrentUser.Expert.PublicName);
            }

            if (!isThreadExpert && (post.Type == PostType.Attachment))
                Email.Send<ThreadExpertAddAttachmentEmail>(thread);

            if (isThreadExpert && (post.Type == PostType.Attachment))
                Email.Send<ThreadUserAddAttachmentEmail>(thread);

            if (AuthenticationHelper.CurrentUser == thread.Author && (post.Type != PostType.Attachment))
            {
                Email.Send<ThreadNewPostToExpertEmail>(thread);
                EventLog.Event<AuthorAddedPostEvent>(thread);
            }

            if (post.Type == PostType.ModeratorAnswer)
            {
                Email.Send<NewModeratorPostUserEmail>(thread);

                if (thread.Expert != null)
                    Email.Send<NewModeratorPostExpertEmail>(thread);
            }

            if (post.Type == PostType.ExpertPM && thread.Expert != null)
                Email.Send<NewPMToExpertEmail>(thread);

            Flash.Success(Resources.Thread.PostCreated);
            return RedirectToAction(MVC.Thread.ThreadDetails(model.ThreadId));
        }

        [DefaultRouting]
        public virtual ActionResult GetAllPosts(int threadId)
        {
            var thread = Repository.Thread.Get(threadId);

            if (!IsAuthorExpertOrModerator(thread))
            {
                FormsAuthentication.RedirectToLoginPage();
                return null;
            }

            return PartialView(MVC.Thread.Views._Thread, new ThreadDetailsModel { Thread = thread });
        }

        [DefaultRouting]
        [AuthorizeRoles(Role.Expert)]
        public virtual ActionResult GetPriceProposalNotification(int threadId)
        {
            var thread = Repository.Thread.Get(threadId);
            return ShowPriceProposalNotification(thread);
        }

        [DefaultRouting]
        [AuthorizeRoles(Role.Expert)]
        public virtual ActionResult ShowPriceProposalNotification(Thread thread)
        {
            return PartialView(MVC.Thread.Views._PriceProposalNotification, thread);
        }

        [HttpPost]
        [DefaultRouting]
        [Authorize]
        public virtual ActionResult DateDifference(int threadId, long threadLastModDate)
        {
            var thread = Repository.Thread.Get(threadId);
            var date = thread.LastModificationDate.Ticks;

            if (date == threadLastModDate)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(date, JsonRequestBehavior.AllowGet);
            }
        }

        [DefaultRouting]
        [Authorize]
        public virtual ActionResult GetThreadDetailsMenu(int threadId, bool isModerator = false)
        {
            var thread = Repository.Thread.Get(threadId);

            if (!IsAuthorExpertOrModerator(thread))
            {
                FormsAuthentication.RedirectToLoginPage();
                return null;
            }

            return ShowThreadDetailsMenu(thread, isModerator);
        }

        [Authorize]
        public virtual ActionResult ShowThreadDetailsMenu(Thread thread, bool isModerator = false, Post post = null)
        {
            if (!IsAuthorExpertOrModerator(thread))
            {
                FormsAuthentication.RedirectToLoginPage();
                return null;
            }

            var buttons = new List<ThreadDetailsMenuModel.MenuButtons>();

            //jeśli user jest pytającym , padła już odpowiedź ale nie została jeszcze zaakceptowana, i jest ekspert przypisany => może akceptować 
            if (thread.Author == AuthenticationHelper.CurrentUser
                && thread.Posts.FirstOrDefault(p => p.Type == PostType.Answer) != null
                && thread.State != ThreadState.Closed
                && thread.State != ThreadState.Accepted
                && thread.Expert != null)
            {
                buttons.Add(ThreadDetailsMenuModel.MenuButtons.Accept);
            }

            if (thread.Author == AuthenticationHelper.CurrentUser && !thread.IsPaid)
            {
                buttons.Add(ThreadDetailsMenuModel.MenuButtons.Pay);
                buttons.Add(ThreadDetailsMenuModel.MenuButtons.Delete);
            }

            //jeśli user jest pytającym, a wątek został przejęty => może pisać posty
            if (thread.Author == AuthenticationHelper.CurrentUser && thread.State != ThreadState.Unoccupied && thread.State != ThreadState.Closed)
            {
                buttons.Add(ThreadDetailsMenuModel.MenuButtons.Answer);
            }

            if(thread.Author == AuthenticationHelper.CurrentUser && thread.State != ThreadState.Closed)
            {
                buttons.Add(ThreadDetailsMenuModel.MenuButtons.AttachFile);
            }

            if (isModerator)
            {
                buttons.Add(ThreadDetailsMenuModel.MenuButtons.ExpertPM);
                buttons.Add(ThreadDetailsMenuModel.MenuButtons.ModeratorAnswer);
                buttons.Add(ThreadDetailsMenuModel.MenuButtons.Events);

                if (thread.State == ThreadState.Occupied)
                    buttons.Add(ThreadDetailsMenuModel.MenuButtons.IncreaseExpertValue);
            }

            //jeśli user jest pytającym, a wątek nie został zaakceptowany lub zamknięty
            //if (thread.Author == AuthenticationHelper.CurrentUser && thread.State != ThreadState.Closed && thread.State != ThreadState.Accepted)
            //{
            //}

            //jeśli user jest pytającym, a wątek został zarezerwowany
            if (thread.Author == AuthenticationHelper.CurrentUser && thread.State == ThreadState.Reserved)
            {
                if(!thread.Posts.Any(p => p.Author == thread.Expert.User))
                    buttons.Add(ThreadDetailsMenuModel.MenuButtons.ReleaseReservedQuestion);
            }

            if (AuthenticationHelper.IsExpert && AuthenticationHelper.CurrentUser != thread.Author)
            {
                var expert = AuthenticationHelper.CurrentUser.Expert;

                if (thread.State == ThreadState.Accepted && thread.Expert == expert)
                {
                    buttons.Add(ThreadDetailsMenuModel.MenuButtons.Answer);
                    buttons.Add(ThreadDetailsMenuModel.MenuButtons.AttachFile);
                }

                //jeśli user jest ekspertem,ale nie pytającym i jeśli przejął pytanie lub został zapytany bezpośrednio (reserved)
                if ((thread.State == ThreadState.Occupied || thread.State == ThreadState.Reserved) && thread.Expert == expert)
                {
                    buttons.Add(ThreadDetailsMenuModel.MenuButtons.Answer);
                    buttons.Add(ThreadDetailsMenuModel.MenuButtons.AttachFile);
                    buttons.Add(ThreadDetailsMenuModel.MenuButtons.DetailsRequest);
                    buttons.Add(ThreadDetailsMenuModel.MenuButtons.GiveUp);
                    buttons.Add(ThreadDetailsMenuModel.MenuButtons.ProposeAdditionalService);
                }

                if (thread.State == ThreadState.Unoccupied)
                {
                    buttons.Add(ThreadDetailsMenuModel.MenuButtons.Occupy);
                    if (!thread.PriceProposals.Any(pp => pp.Expert == expert))
                        buttons.Add(ThreadDetailsMenuModel.MenuButtons.PriceProposal);
                }

                buttons.Add(ThreadDetailsMenuModel.MenuButtons.ReportIssue);
            }

            return PartialView(MVC.Thread.Views._ThreadDetailsMenu, new ThreadDetailsMenuModel(thread, buttons, post));
        }

        [Authorize]
        [DefaultRouting]
        public virtual ActionResult DirectQuestion(int expertId, int threadId)
        {
            var expert = Repository.Expert.Get(expertId);

            var thread = Repository.Thread.Get(threadId);

            if (!IsAuthor(thread))
            {
                FormsAuthentication.RedirectToLoginPage();
                return null;
            }

            if (thread.State == ThreadState.Unoccupied)
            {
                thread.Expert = expert;
                thread.State = ThreadState.Reserved;
                Repository.Thread.Update(thread);

                var post = SystemPostFactory.BuildReservedPost(Repository.Consultant.All().First(), expert);
                Repository.Thread.AddPost(thread, post);
                Repository.Thread.Update(thread);

                Email.Send<DirectQuestionEmail>(thread);

                EventLog.Event<NewThreadForExpertEvent>(thread, expert.PublicName);
            }

            return RedirectToAction(MVC.Thread.ThreadDetails(threadId));
        }

        [Authorize]
        public virtual ActionResult ReleaseReservedQuestion(int threadId)
        {
            var thread = Repository.Thread.Get(threadId);

            if(thread.State == ThreadState.Reserved)
            {
                var author = thread.Author;

                thread.Expert = null;
                thread.State = ThreadState.Unoccupied;
                thread.ExpertReleaseDate = null;
                thread.AdditionalExpertProvisionValue = 0;

                var post = SystemPostFactory.BuildReleasedPost(Repository.Consultant.All().First());
                Repository.Thread.AddPost(thread, post);
                Repository.Thread.Update(thread);

                if(thread.Expert != null)
                {
                    thread.Expert = null;
                    Repository.Thread.Update(thread);
                }
            }

            Flash.Success(Resources.Thread.ThreadDirectQuestionReleased);
            return RedirectToAction(MVC.Thread.ThreadDetails(threadId));
        }

        public void MoveAttachmentsFromTemporaryFolder(string temporaryAttachmentFolder, int threadId)
        {
            var attachmentsDirPath = HttpContext.Server.MapPath("~/Attachments/");
            Directory.SetCurrentDirectory(attachmentsDirPath);
            
            var destinationDir = "Thread_" + threadId;

            Directory.Move(attachmentsDirPath + temporaryAttachmentFolder, attachmentsDirPath + destinationDir);
            
            var thread = Repository.Thread.Get(threadId);

            var post = new Post {   
                                    Attachments = new List<Attachment>(),
                                    Author = thread.Author,
                                    Type = PostType.Attachment,
                                    Content = "Pytanie zawiera załączniki"
                                };

            var directoryInfo = new DirectoryInfo(attachmentsDirPath + destinationDir);
            foreach (var file in directoryInfo.GetFiles())
            {
                var attachment = new Attachment
                {
                    AttachmentName = file.Name,
                    AttachmentPath = string.Format("\\Attachments\\{0}\\{1}", destinationDir, file.Name),
                    Author = thread.Author,
                    ContentType = file.Extension,
                    AttachmentSize = (int)file.Length
                };

                attachment.Type = UploadHelper.SpecifyAttachmentType(file.Extension);

                post.Attachments.Add(attachment);
            }

            Repository.Thread.AddPost(thread, post);
        }

        [HttpPost]
        [Authorize]
        [DefaultRouting]
        public virtual ActionResult ThreadAttachmentUpload(int threadId)
        {
            var post = new Post { Attachments = new List<Attachment>() };
            var user = AuthenticationHelper.CurrentUser;
            var thread = Repository.Thread.Get(threadId);

            if (!IsAuthorExpertOrModerator(thread))
            {
                FormsAuthentication.RedirectToLoginPage();
                return null;
            }

            for (int fileCount = 0; fileCount < Request.Files.Count; fileCount++)
            {
                var file = Request.Files[fileCount];

                if (UploadHelper.ValidateUpload(file.ContentLength, file.ContentType, UploadHelper.FileType.PostAttachment))
                {
                    var attachmentCount = thread.Posts.Sum(p => p.Attachments.Count());
                    var attachmentPath = string.Format("\\Attachments\\Thread_{0}\\{1}{2}", thread.Id, attachmentCount, file.FileName);

                    var attachmentsDirPath = HttpContext.Server.MapPath("~/Attachments/");
                    Directory.SetCurrentDirectory(attachmentsDirPath);

                    Directory.CreateDirectory("Thread_" + thread.Id);

                    var filePath = Path.Combine(HttpContext.Server.MapPath(attachmentPath));

                    file.SaveAs(filePath);

                    if (fileCount == 0)
                    {
                        post.Author = user;
                        post.Content = Request.Files.Count == 1 ? Resources.Attachments.AttachmentAdded : Resources.Attachments.AttachmentsAdded;
                        post.Type = PostType.Attachment;
                    }

                    if (fileCount == 0 && thread.Posts.Last().Author == user && thread.Posts.Last().Type == PostType.Attachment)
                    {
                        post = thread.Posts.Last();
                    }

                    var attachment = new Attachment
                    {
                        AttachmentName = file.FileName,
                        AttachmentPath = attachmentPath,
                        Author = user,
                        ContentType = file.ContentType,
                        AttachmentSize = file.ContentLength
                    };

                    attachment.Type = UploadHelper.SpecifyAttachmentType(file.ContentType);

                    if (fileCount == 0 && (thread.Posts.Last().Type != PostType.Attachment || thread.Posts.Last().Author != AuthenticationHelper.CurrentUser))
                    {
                        Repository.Thread.AddPost(thread, post);
                    }
                    post.Attachments.Add(attachment);
                    Repository.Thread.Update(thread);

                    Flash.Success(Resources.Attachments.AttachmentAdded + " " + attachment.AttachmentName + " " + UploadHelper.RoundBytes(attachment.AttachmentSize, 2));

                    return null;
                }
            }

            Flash.Error(Resources.Attachments.AttachmentSizeError);

            return new HttpStatusCodeResult(400);
        }

        [DefaultRouting]
        [HttpPost]
        public virtual ActionResult PrecedingUpload(string temporaryAttachmentFolder)
        {
            for (int fileNumber = 0; fileNumber < Request.Files.Count; fileNumber++)
            {
                var file = Request.Files[fileNumber];
                if (UploadHelper.ValidateUpload(file.ContentLength, file.ContentType, UploadHelper.FileType.PostAttachment))
                {
                    var attachmentPath = string.Format("\\Attachments\\{0}\\{1}", temporaryAttachmentFolder, file.FileName);
                    var attachmentsDirPath = HttpContext.Server.MapPath("~/Attachments/");
                    Directory.SetCurrentDirectory(attachmentsDirPath);

                    Directory.CreateDirectory(temporaryAttachmentFolder);

                    var filePath = Path.Combine(HttpContext.Server.MapPath(attachmentPath));

                    file.SaveAs(filePath);

                    return null;
                }
            }

            return new HttpStatusCodeResult(400);
        }

        [DefaultRouting]
        public virtual ActionResult AttachmentTiles(string temporaryAttachmentFolder, bool isTiny, bool allowNoAttachments = false)
        {
            var attachmentsDirPath = HttpContext.Server.MapPath("~/Attachments/");
            
            if (allowNoAttachments && !Directory.Exists(attachmentsDirPath + temporaryAttachmentFolder))
            {
                return null;
            }

            var files = new AttachmentTiles { TemporaryAttachmentFolder = temporaryAttachmentFolder, FileNames = new List<string>(), IsTiny = isTiny};

            var directoryInfo = new DirectoryInfo(attachmentsDirPath + temporaryAttachmentFolder);
            foreach (var file in directoryInfo.GetFiles())
            {
                files.FileNames.Add(file.Name);
            }

            return PartialView(MVC.Thread.Views._AttachmentTiles, files);
        }

        [DefaultRouting]
        [HttpPost]
        public virtual ActionResult DeleteAttachmentTile(string fileName, string temporaryAttachmentFolder)
        {
            var attachmentsDirPath = HttpContext.Server.MapPath("~/Attachments/");
            var directoryInfo = new DirectoryInfo(attachmentsDirPath + temporaryAttachmentFolder);
            foreach (var file in directoryInfo.GetFiles())
            {
                if(file.Name == fileName)
                {
                    file.Delete();
                }
            }

            if(directoryInfo.GetFiles().Count() == 0)
            {
                directoryInfo.Delete();   
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [DefaultRouting]
        [HttpPost]
        public virtual ActionResult GetTemporaryAttachmentsCount(string temporaryAttachmentFolder)
        {
            var attachmentsDirPath = HttpContext.Server.MapPath("~/Attachments/");
            var directoryInfo = new DirectoryInfo(attachmentsDirPath + temporaryAttachmentFolder);

            if(!directoryInfo.Exists)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

            var title = Resources.Attachments.Files;
            var count = directoryInfo.GetFiles().Count();
            if(count == 1)
            {
                title = Resources.Attachments.File;
            }

            return Json(directoryInfo.GetFiles().Count() + " " + title, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult GetAttachment(int attachmentId)
        {
            var attachment = Repository.Thread.GetAttachment(attachmentId);
            var thread = attachment.Post.Thread;
            var user = AuthenticationHelper.CurrentUser;

            var isAuthorized = user != null && (thread.Author == user || user.IsExpert || user.IsModerator);
            var isSanitized = thread.SanitizationStatus == ThreadSanitizationStatus.Sanitized && attachment.Post.IsPubliclyVisible;

            var canView = isAuthorized || isSanitized;

            if (!canView)
            {
                FormsAuthentication.RedirectToLoginPage();
                return null;
            }

            return File(Server.MapPath(attachment.AttachmentPath), attachment.ContentType);
        }

        [Authorize]
        public virtual ActionResult DeletePost(int postId)
        {
            var post = Repository.Thread.GetPost(postId);
            var thread = post.Thread;

            foreach (var attachment in post.Attachments)
                attachment.Type = AttachmentType.Hidden;

            post.Type = PostType.Hidden;

            Repository.Thread.Update(thread);

            Flash.Success(Resources.Thread.PostDeleteSuccess);

            return RedirectToAction(MVC.Thread.ThreadDetails(thread.Id));
        }

        [Authorize]
        public virtual ActionResult DeleteAttachment(int attachmentId)
        {
            var attachment = Repository.Thread.GetAttachment(attachmentId);
            var thread = attachment.Post.Thread;
            var post = attachment.Post;

            attachment.Type = AttachmentType.Hidden;
            if (post.Attachments.Count(p => p.Type != AttachmentType.Hidden) == 0) { post.Type = PostType.Hidden; }

            Repository.Thread.Update(thread);

            Flash.Success(Resources.Attachments.DeletedSuccess);

            return RedirectToAction(MVC.Thread.ThreadDetails(thread.Id));
        }

        [AuthorizeRoles(Role.Expert)]
        public virtual ActionResult OccupyThread(int threadId)
        {
            var thread = Repository.Thread.Get(threadId);

            //inner hack
            if (AuthenticationHelper.IsExpert && thread.IsInner)
            {
                if (!AuthenticationHelper.CurrentUser.Expert.IsInner)
                {
                    Flash.Error(Resources.Thread.InnerThreadError);
                    return RedirectToAction(MVC.StaticPages.Home());
                }
            }

            if (thread.State == ThreadState.Unoccupied && AuthenticationHelper.IsExpert)
            {
                thread.Expert = AuthenticationHelper.CurrentUser.Expert;
                thread.State = ThreadState.Occupied;
                thread.ExpertReleaseDate = DateTime.Now.AddMinutes(15);
                Repository.Thread.Update(thread);

                Repository.Thread.AddPost(thread, SystemPostFactory.BuildAnalyzingPost(Repository.Consultant.All().First(), thread.Expert));
            }

            EventLog.Event<ThreadOccupiedEvent>(thread, AuthenticationHelper.CurrentUser.Expert.PublicName);

            Flash.Success(Resources.Thread.ThreadOccupied);
            return RedirectToAction(MVC.Thread.ThreadDetails(threadId));
        }

        [AuthorizeRoles(Role.Expert)]
        [DefaultRouting]
        public virtual ActionResult ExtendOccupyThreadLockTime(int threadId)
        {
            var thread = Repository.Thread.Get(threadId);

            if (thread.ExpertReleaseDate != null)
            {
                thread.ExpertReleaseDate = DateTime.Now.AddMinutes(15);
                Repository.Thread.Update(thread);
            }
            else
            {
                Flash.Error(Resources.Thread.ExtendThreadLockTimeFailure);
            }

            return PartialView(MVC.Thread.Views._ReleaseTimer, thread.ExpertReleaseDate);
        }

        [AuthorizeRoles(Role.Expert)]
        public virtual ActionResult GiveUp(int threadId)
        {
            return PartialView(new GiveUpForm());
        }

        [AuthorizeRoles(Role.Expert)]
        [HttpPost]
        public virtual ActionResult GiveUp(GiveUpForm model)
        {
            var thread = Repository.Thread.Get(model.ThreadId);

            if (thread.Expert != AuthenticationHelper.CurrentUser.Expert)
            {
                FormsAuthentication.RedirectToLoginPage();
                return null;
            }

            if (thread.State == ThreadState.Occupied || thread.State == ThreadState.Reserved)
            {
                var expert = thread.Expert;

                thread.Expert = null;
                thread.State = ThreadState.Unoccupied;
                thread.ExpertReleaseDate = null;
                thread.AdditionalExpertProvisionValue = 0;

                if (thread.AcceptedPriceProposal != null)
                {
                    //kupującemu zwalnia się część środków o które pomniejszyła się wartość wątku
                    var surchargeValue = thread.GetSurchargeValue(thread.AcceptedPriceProposal.Id);
                    thread.AddTransfer(Transfer.Pending(surchargeValue, string.Format(Resources.Payment.TransferThreadPaymentSurgargeReturnText, thread.Name)), thread.Author);
                    thread.Value -= surchargeValue;
                    Repository.Thread.DeletePriceProposal(thread.AcceptedPriceProposal);
                    Repository.User.Update(thread.Author);
                }
                Repository.Thread.Update(thread);

                if (thread.Posts.Any(p => p.Author == expert.User))
                {
                    var post = SystemPostFactory.BuildGiveUpPost(Repository.Consultant.All().First(), model.Comment);
                    Repository.Thread.AddPost(thread, post);

                    if(thread.Posts.Any(p => p.Type == PostType.Analyzing))
                        Repository.Thread.DeletePost(thread.Posts.Single(p => p.Type == PostType.Analyzing));

                    Email.Send<GiveUpEmail>(thread, expert);
                    EventLog.Event<GiveUpEvent>(thread, AuthenticationHelper.CurrentUser.Expert.PublicName);
                }
                else
                {
                    var analyzingPost = thread.Posts.Single(p => p.Type == PostType.Analyzing);
                    Repository.Thread.DeletePost(analyzingPost);
                }

                EventLog.Event<ThreadReleasedEvent>(thread, expert.PublicName);
            }

            Flash.Info(Resources.Thread.ThreadGivenUp);
            return RedirectToAction(MVC.Thread.AvailableQuestionList());
        }

        [Authorize]
        public virtual ActionResult DeleteThread(int id)
        {
            var thread = Repository.Thread.Get(id);
            if (!IsAuthor(thread) || thread.IsPaid)
            {
                FormsAuthentication.RedirectToLoginPage();
                return null;
            }

            thread.State = ThreadState.Hidden;
            Repository.Thread.Update(thread);
            
            return RedirectToAction(MVC.Profile.MyQuestions());
        }

        [Authorize]
        [DefaultRouting]
        public virtual ActionResult CreateFeedback(int threadId)
        {
            return PartialView(new FeedbackForm{ThreadId = threadId});
        }

        [HttpPost]
        [Authorize]
        [DefaultRouting]
        public virtual ActionResult CreateFeedbackForm(FeedbackForm form, int? threadId = null, bool skipAnswerFeedback = false)
        {
            Thread thread;

            if(threadId == null)
                thread = Repository.Thread.Get(form.ThreadId);
            else
                thread = Repository.Thread.Get((int)threadId);

            if (!IsAuthor(thread))
            {
                FormsAuthentication.RedirectToLoginPage();
                return null;
            }

            if ((!ModelState.IsValid || form.Mark == 0) && skipAnswerFeedback == false)
                return PartialView(MVC.Thread.Views.CreateFeedback, form);

            if (thread.State == ThreadState.Occupied || thread.State == ThreadState.Reserved)
            {
                thread.State = ThreadState.Accepted;
                thread.ThreadAcceptanceDate = DateTime.Now;
                thread.HandleProvisions();
            }

            Repository.Thread.Update(thread);

            //zwolnienie płatności wstępnej
            thread.AddTransfer(Transfer.Pending(thread.Value, string.Format(Resources.Payment.TransferThreadPaymentPendingReleaseText, thread.Name)), AuthenticationHelper.CurrentUser);
            //pobranie płatności końcowej
            thread.AddTransfer(Transfer.Cash(-thread.Value, string.Format(Resources.Payment.TransferAcceptAnswerFeeText, thread.Name)), AuthenticationHelper.CurrentUser);
            //wypłata wynagrodzenia dla eksperta z tytułu akceptacji
            thread.AddTransfer(Transfer.Cash(thread.FullExpertProvisionValue(), string.Format(Resources.Payment.TransferAcceptAnswerExpertProvisionText, thread.Name)), thread.Expert.User);

            //wypłata prowizji dla pośrednika
            if (thread.BrokerProvision > decimal.Zero)
            {
                thread.AddTransfer(Transfer.Cash(thread.BrokerProvisionValue(), string.Format(Resources.Payment.TransferAcceptAnswerBrokerProvisionText, thread.Name)), thread.Broker);
                Repository.User.Update(thread.Broker);
            }

            if (thread.Expert.Recommendation != null && thread.Expert.AcceptedAnswers >= 4)
            {
                if (thread.Expert.Recommendation.Recommender != null && thread.Expert.Recommendation.DidRecommenderReceiveBonus == false)
                {
                    var transfer = new Transfer
                    {
                        OrderDate = DateTime.Now,
                        TransferDate = DateTime.Now,
                        Title = Resources.Payment.RecommenderBonus,
                        Payment = null,
                        Value = 20,
                        IsPending = false,
                        Owner = thread.Expert.Recommendation.Recommender
                    };
                    thread.Expert.Recommendation.DidRecommenderReceiveBonus = true;

                    Repository.Transfer.Add(transfer);
                    Repository.Transfer.Update(transfer);
                    Repository.Recommendation.Update(thread.Expert.Recommendation);
                }
            }

            Repository.Thread.Update(thread);
            Repository.User.Update(AuthenticationHelper.CurrentUser);
            Repository.User.Update(thread.Expert.User);

            Email.Send<ThreadAnswerAcceptedEmail>(thread);
            EventLog.Event<AuthorAcceptedAnswerEvent>(thread, thread.Expert.PublicName);

            Flash.Success(Resources.Thread.AnswerAccepted);

            if(!skipAnswerFeedback)
            {
                var feedback = Mapper.Map<Feedback>(form);
                Repository.Thread.AddFeedback(thread, feedback);
                Repository.Expert.UpdateExpertStats(thread.Expert);
                Repository.User.UpdateUserStats(thread.Author);

                Flash.Success(Resources.Thread.FeedbackAdded);
                EventLog.Event<AuthorCreatedFeedbackEvent>(thread, thread.Expert.PublicName);
            }

            return null;
        }

        [AuthorizeRoles(Role.Expert)]
        public virtual ActionResult ReportIssueForm(int threadId)
        {
            return PartialView(new ReportIssueForm { ThreadId = threadId });
        }

        [AuthorizeRoles(Role.Expert)]
        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult ReportIssue(ReportIssueForm form)
        {
            if (string.IsNullOrWhiteSpace(form.Comment))
            {
                var errorMessage = "";
                switch (form.IssueType)
                {
                    case ThreadIssueType.Other: errorMessage = Resources.Thread.ReportIssueCommentOtherIssueObligatory; break;
                    case ThreadIssueType.InvalidCategory: errorMessage = Resources.Thread.ReportIssueCommentCategoryObligatory; break;
                    case ThreadIssueType.Duplicate: errorMessage = Resources.Thread.ReportIssueCommentDuplicatedObligatory; break;
                }

                ModelState.AddModelError("Comment", errorMessage);
                return PartialView(MVC.Thread.Views.ReportIssueForm, form);
            }

            var issue = Mapper.Map<ThreadIssue>(form);
            Repository.Thread.AddIssue(issue.Thread, issue);

            EventLog.Event<ThreadIssueReportEvent>(issue.Thread, issue.IssueType.Describe(Resources.Thread.ResourceManager),
                                              issue.Id);

            Flash.Success(Resources.Thread.IssueReported);
            return null;
        }

        [DefaultRouting]
        public virtual ActionResult CategoryExpertsOnlineInfo(int categoryId)
        {
            var expertsCount = ActiveUsersHelper.GetActiveAndPublicExpertsCountForCategory(categoryId);
            if (expertsCount == 0)
                return null;

            var infoTemplate = expertsCount > 1
                                      ? Resources.Thread.CategoryExpertsOnlineInfoPlural
                                      : Resources.Thread.CategoryExpertsOnlineInfoSingular;
            var info = string.Format(infoTemplate, expertsCount);

            return Json(info, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeRoles(Role.Expert)]
        public virtual ActionResult PriceProposalForm(int data, decimal defaultValue)
        {
            return PartialView(new PriceProposalForm
                                   {
                                       ThreadId = data,
                                       SurchargeValue = defaultValue
                                   });
        }

        [AuthorizeRoles(Role.Expert)]
        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult PriceProposal(PriceProposalForm form)
        {
            if (!ModelState.IsValid)
                return PartialView(MVC.Thread.Views.PriceProposalForm, form);

            var priceProposal = Mapper.Map<PriceProposal>(form);

            if (priceProposal.SurchargeValue <= priceProposal.Thread.Value)
            {
                ModelState.AddModelError("SurchargeValue", Resources.Thread.PriceProposalMustBeHigher);
                return PartialView(MVC.Thread.Views.PriceProposalForm, form);
            }

            priceProposal.Expert = AuthenticationHelper.CurrentUser.Expert;

            Repository.Thread.AddPriceProposal(priceProposal.Thread, priceProposal);

            EventLog.Event<ExpertPriceProposalEvent>(priceProposal.Thread, AuthenticationHelper.CurrentUser.Expert.PublicName, priceProposal.Id);

            Flash.Success(Resources.Thread.PriceProposalSent);

            return null;

        }

        [DefaultRouting]
        public virtual ActionResult UserDefinedPriceForm(ThreadForm form)
        {
            return PartialView(new UserDefinedPriceForm{});
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult UserDefinedPrice(UserDefinedPriceForm form)
        {
            if (!ModelState.IsValid)
                return PartialView(MVC.Thread.Views.UserDefinedPriceForm, form);

            if (form.UserDefinedPrice < 5)
            {
                ModelState.AddModelError("UserDefinedPrice", Resources.Thread.UserDefinedPriceMustBeHigher);
                return PartialView(MVC.Thread.Views.UserDefinedPriceForm, form);
            }

            return null;
        }

        [AuthorizeRoles(Role.Expert)]
        public virtual ActionResult ProposeAdditionalService(int threadId)
        {
            return PartialView(new AdditionalServiceForm());
        }

        [HttpPost]
        [AuthorizeRoles(Role.Expert)]
        [DefaultRouting]
        public virtual ActionResult ProposeAdditionalService(AdditionalServiceForm form)
        {
            if(!ModelState.IsValid)
            {
                return PartialView(MVC.Thread.Views.ProposeAdditionalService, form);
            }

            var thread = Repository.Thread.Get(form.ThreadId);

            var additionalService = new AdditionalService
                                        {
                                            Title = form.Title,
                                            Comment = form.Comment,
                                            CreationDate = DateTime.Now,
                                            Expert = thread.Expert,
                                            IsVerified = false,
                                            LastModificationDate = DateTime.Now,
                                            Thread = thread,
                                            Value = ThreadValueHelper.CalculateUserValue(form.Value.Value, thread.Expert)
                                        };

            thread.AdditionalServices.Add(additionalService);
            Repository.Thread.Update(thread);

            Flash.Success(Resources.Thread.AdditionalServiceSuccess);
            EventLog.Event<NewAdditionalServiceEvent>(thread, thread.Expert.PublicName, additionalService.Id);
            Email.Send<AdditionalServiceEmail>(additionalService);

            return null;
        }

        [Authorize]
        [ChildActionOnly]
        public virtual ActionResult AdditionalServiceInformation(int threadId)
        {
            var thread = Repository.Thread.Get(threadId);

            var model = new AdditionalServiceInformation {Thread = thread};
            model.AdditionalServices = thread.Author == AuthenticationHelper.CurrentUser ? thread.VerifiedAdditionalServices : thread.AdditionalServices;

            return PartialView(model);
        }

        [Authorize]
        public virtual ActionResult DeclineAdditionalService(int additionalServiceId)
        {
            var additionalService = Repository.Thread.GetAdditionalService(additionalServiceId);
            var thread = additionalService.Thread;

            Repository.Thread.DeclineAdditionalService(additionalServiceId);

            Flash.Success(Resources.Thread.AdditionalServiceDeclined);

            Email.Send<AdditionalServiceAnswerEmail>(additionalService);

            return RedirectToAction(MVC.Thread.ThreadDetails(thread.Id));
        }

        [DefaultRouting]
        [Authorize]
        [HttpPost]
        public virtual ActionResult GetThreadDetailsByStatus(int threadId, int threadIntState)
        {
            var thread = Repository.Thread.Get(threadId);

            return Json(thread.IntState == threadIntState ? 0 : 1, JsonRequestBehavior.AllowGet);
        }

        [DefaultRouting]
        [AuthorizeRoles(Role.Moderator)]
        public virtual ActionResult IncreaseExpertValueForm(int threadId)
        {
            var thread = Repository.Thread.Get(threadId);
            return PartialView(new IncreaseExpertValueForm
                                   {
                                       ThreadId = threadId,
                                       NewExpertValue = thread.CalculatePotentialExpertValue(thread.Expert)
                                   }
                );
        }

        [DefaultRouting]
        [HttpPost]
        [AuthorizeRoles(Role.Moderator)]
        public virtual ActionResult IncreaseExpertValue(IncreaseExpertValueForm form)
        {
            var thread = Repository.Thread.Get(form.ThreadId);
            if (form.NewExpertValue <= thread.CalculatePotentialExpertValue(thread.Expert))
                ModelState.AddModelError("NewExpertValue", Resources.Thread.ExpertValueMustBeHigher);

            if (!ModelState.IsValid)
                return PartialView(MVC.Thread.Views.IncreaseExpertValueForm, form);

            thread.CalculateAdditionalExpertProvision(form.NewExpertValue.Value);
            Repository.Thread.Update(thread);

            return null;
        }


        [DefaultRouting]
        [Authorize]
        public virtual ActionResult DisplaySelectedCategoryAttributes(int threadId)
        {
            var thread = Repository.Thread.Get(threadId);

            if (IsAuthorExpertOrModerator(thread))
                return PartialView(MVC.Thread.Views._DisplaySelectedCategoryAttributes, thread.GetCategoryAttributesForDisplay());

            throw new AccessViolationException();
        }

        [DefaultRouting]
        public virtual ActionResult Question(ThreadForm form)
        {
            var model = new Question
                            {
                                ThreadForm = form,
                                Category = Repository.Category.Get(form.CategoryId.Value)
                            };

            return PartialView(MVC.Thread.Views._Question, model);
        }

        private void PrepareOptions(ThreadForm model)
        {
            model.Priority = ThreadPriority.Medium;
            model.Verbosity = ThreadVerbosity.Medium;
            SetValue(model);

            ThreadMemoryHelper.RememberThread(model);
        }

        private bool IsAuthor(Thread thread)
        {
            return AuthenticationHelper.CurrentUser == thread.Author;
        }

        private bool IsAuthorExpertOrModerator(Thread thread)
        {
            return AuthenticationHelper.IsExpert || AuthenticationHelper.IsModerator || IsAuthor(thread);
        }
    }
}
