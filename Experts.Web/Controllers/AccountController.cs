using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using Experts.Core.Entities;
using Experts.Core.Entities.Events;
using Experts.Core.Exceptions;
using Experts.Core.Utils;
using Experts.Web.Filters;
using Experts.Web.Models.Account;
using Experts.Web.Models.Forms;
using Experts.Web.Helpers;
using Experts.Web.Models.Shared;


namespace Experts.Web.Controllers
{
    public partial class AccountController : BaseController
    {
        public virtual ActionResult SignUp()
        {
            if (AuthenticationHelper.IsNoSignUpUser)
            {
                var email = AuthenticationHelper.CurrentUser.Email;
                var model = new ProfileForm {
                                    EmailForm = new EmailForm { Email = email }
                                };
                return View(model);
            }

            return View();
        }

        [HttpPost]
        public virtual ActionResult SignUp(ProfileForm model)
        {
            if (ModelState.IsValid)
            {
                var isNoSignUpUser = AuthenticationHelper.IsNoSignUpUser;

                User user;
                if (isNoSignUpUser)
                {
                    user = AuthenticationHelper.CurrentUser;

                    Mapper.Map(model, user);
                    Repository.User.TransformNoSignUpAccount(user);
                    AuthenticationHelper.Authenticate(user.Email);
                }
                else if (Repository.User.FindByEmail(model.EmailForm.Email) != null)
                {
                    user = Repository.User.FindByEmail(model.EmailForm.Email);

                    Mapper.Map(model, user);
                    Repository.User.TransformNoSignUpAccount(user);
                    AuthenticationHelper.Authenticate(user.Email);
                }
                else
                {
                    user = Mapper.Map<User>(model);

                    Repository.User.Add(user);

                    Repository.User.AddEmailConfigurationDefaultValue(user);
                }

                var recommendation = Repository.Recommendation.FindByRecommenderEmail(model.EmailForm.Email);
                if (recommendation != null)
                {
                    recommendation.Recommender = user;
                    Repository.Recommendation.Update(recommendation);
                }

                Flash.Success(Resources.Account.AccountCreated);

                Email.Send<ActivationEmail>(user);
                Log.Event<UserRegisteredEvent>(user);


                if (isNoSignUpUser)
                    return RedirectToAction(MVC.StaticPages.Home());

                return RedirectToAction(MVC.Account.SignIn());
            }

            return View(model);
        }

        public virtual ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult SignIn(SignInForm model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = Repository.User.VerifyCredentials(model.Login, model.Password);
                    AuthenticationHelper.Authenticate(user.Email);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        return Redirect(returnUrl);

                    if (ThreadMemoryHelper.IsThreadRemembered())
                        return RedirectToAction(MVC.Thread.Options());

                    if (user.IsExpert && user.Expert.Categories.Count > user.Expert.CategoryAttributes.Count)
                        return RedirectToAction(MVC.Account.ExpertCategoryAttributes());

                    return RedirectToAction(MVC.StaticPages.Home());
                }
                catch (AuthenticationException)
                {
                    Flash.Error(Resources.Account.AuthenticationFailed);
                }
                catch (UserAccountNotActivatedException)
                {
                    Flash.Error(Resources.Account.AccountNotActivated);
                    model.ResendActivationMail = true;
                }
            }

            return View(model);
        }

        [DefaultRouting]
        public virtual ActionResult QuickSignIn()
        {
            return PartialView(new QuickSignInForm());
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult QuickSignInForm(QuickSignInForm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = Repository.User.VerifyCredentials(model.Form.Login, model.Form.Password);
                    AuthenticationHelper.Authenticate(user.Email);

                    string redirect = null;
                    if (user.IsExpert && user.Expert.Categories.Count > user.Expert.CategoryAttributes.Count)
                        redirect = Url.Action(MVC.Account.ExpertCategoryAttributes());

                    return PartialView(MVC.Shared.Views._TopMenu, redirect);
                }
                catch (AuthenticationException)
                {
                    model.ErrorMessage = Resources.Account.AuthenticationFailed;
                    model.ShowSignUpOrResetPasswordMessage = true;
                }
                catch (UserAccountNotActivatedException)
                {
                    model.ErrorMessage = Resources.Account.AccountNotActivated;
                    model.ShowResendActivationMailInfo = true;
                }
            }

            return PartialView(MVC.Account.Views.QuickSignIn, model);
        }

        public virtual ActionResult KeySignIn(string key)
        {
            try
            {
                var user = Repository.User.VerifyCredentials(key);
                AuthenticationHelper.Authenticate(user.Email);
            }
            catch (UserNotFoundException)
            {
                Flash.Error(Resources.Account.UserNotFound);
            }

            return RedirectToAction(MVC.StaticPages.Home());
        }

        [Authorize]
        public virtual ActionResult SignOut()
        {
            //chat signout
            var chatController = new ChatController();
            chatController.ControllerContext = ControllerContext;
            chatController.FinishChat();

            FormsAuthentication.SignOut();

            return RedirectToAction(MVC.StaticPages.Home());
        }

        [DefaultRouting]
        public virtual ActionResult ExpertCategoryAttributesPopup(int categoryId, int expertId)
        {
            var expert = Repository.Expert.Get(expertId);
            var category = Repository.Category.Get(categoryId);

            var attributeValues = expert.CategoryAttributes.SingleOrDefault(ca => ca.Category.Id == categoryId);

            string categoryAttributeNames = null;

            if(attributeValues != null)
            {
                foreach (var categoryAttribute in attributeValues.CategoryAttributes)
                {
                    foreach(var option in categoryAttribute.SelectedOptions)
                    {
                        categoryAttributeNames += option.Value + "<br/>";
                    }
                }

                if (!string.IsNullOrEmpty(attributeValues.ExpertReason))
                {
                    categoryAttributeNames += Resources.Administration.ExpertCompetence + "<br/>" + attributeValues.ExpertReason;
                }
            }

            if (string.IsNullOrEmpty(categoryAttributeNames))
            {
                categoryAttributeNames = Resources.Administration.EmptyExpertCompetence;
            }

            return Content(categoryAttributeNames);
        }

        [AuthorizeRoles(Role.Expert)]
        public virtual ActionResult ExpertCategoryAttributes(int? categoryId = null)
        {
            var currentExpert = AuthenticationHelper.CurrentUser.Expert;

            Category category;
            var categoryAttributeValues = new List<AttributeValueModel>();

            if(categoryId.HasValue)
            {
                category = Repository.Category.Get(categoryId.Value);
                var attributeValues = currentExpert.CategoryAttributes.SingleOrDefault(ca => ca.Category.Id == categoryId);

                if(attributeValues != null)
                {
                    var attributeValueModels =
                        CategoryAttributeHelper.GetCategoryAttributeValueModel(attributeValues.CategoryAttributes, true);

                    categoryAttributeValues.AddRange(attributeValueModels);
                }
            }
            else
            { 
                var notFilledCategoried = currentExpert.Categories.Where(c => currentExpert.CategoryAttributes.All(ca => ca.Category != c));
                category = notFilledCategoried.FirstOrDefault();
                if (category == null)
                    return RedirectToAction(MVC.Profile.Edit());
            }

            string expertReason = "";

            if (currentExpert.CategoryAttributes.Where(c => c.Category.Id == category.Id).Count() != 0)
                    expertReason = currentExpert.CategoryAttributes.Where(c => c.Category.Id == category.Id).FirstOrDefault().ExpertReason;  

            var attributes = category.Attributes.Where(ca => ca.Type == CategoryAttributeType.MultiSelect || ca.Type == CategoryAttributeType.SingleSelect).ToList();

            var model = new ExpertCategoryAttributesModel { Category = category, CategoryAttributes = attributes, ExpertReason = expertReason };
            if (categoryAttributeValues != null)
                model.CategoryAttributeValues = categoryAttributeValues.ToArray();

            return View(model);
        }

        [HttpPost]
        [AuthorizeRoles(Role.Expert)]
        public virtual ActionResult ExpertCategoryAttributes(int categoryId, IEnumerable<AttributeValueModel> attributeValues, string expertReason)
        {
            var currentExpert = AuthenticationHelper.CurrentUser.Expert;

            var helper = new CategoryAttributeHelper();
            var category = Repository.Category.Get(categoryId);
            var categoryAttributeValues = new ExpertCategoryAttributeValues { Category = category };

            categoryAttributeValues.CategoryAttributes.AddRange(helper.GetCategoryAttributeValues(attributeValues, true));
            categoryAttributeValues.ExpertReason = expertReason;
            
            var currentCategoryAttributeValues = currentExpert.CategoryAttributes.SingleOrDefault(c => c.Category.Id == categoryId);

            if (currentCategoryAttributeValues != null)
               currentExpert.CategoryAttributes.Remove(currentCategoryAttributeValues);

            currentExpert.CategoryAttributes.Add(categoryAttributeValues);
            Repository.Expert.Update(currentExpert);

            Log.ExpertQualificationChangedEvent(currentExpert.User, string.Format(Resources.Account.ExpertCategoryAttributesChanged, category.Name));

            return RedirectToAction(currentExpert.CategoryAttributes.Count < currentExpert.Categories.Count ? MVC.Account.ExpertCategoryAttributes() : MVC.Profile.Edit());
        }

        [DefaultRouting]
        [AuthorizeRoles(Role.Expert)]
        public virtual ActionResult ChildCategoryAttributes([Bind(Prefix = "AttributeValues")]  AttributeValueModel[] attributeValues, int attributeId)
        {
            return GetChildCategoryAttributesActionResult(attributeValues, attributeId);
        }

        [DefaultRouting]
        [AuthorizeRoles(Role.Expert)]
        [HttpPost]
        public virtual ActionResult ChildCategoryAttributesPost([Bind(Prefix = "AttributeValues")]  AttributeValueModel[] attributeValues, int attributeId)
        {
            return GetChildCategoryAttributesActionResult(attributeValues, attributeId);
        }

        private ActionResult GetChildCategoryAttributesActionResult(AttributeValueModel[] attributeValues, int attributeId)
        {
            var helper = new CategoryAttributeHelper();
            var categoryAttributeValues = helper.GetCategoryAttributeValues(attributeValues.Where(cv => cv.AttributeId == attributeId), true);
            var allSubAttributes = categoryAttributeValues.SelectMany(cav => cav.Attribute.ChildAttributes);
            var selectedSubAttributes = allSubAttributes.Where(sa => categoryAttributeValues.Any(cav => cav.SelectedOptions.Intersect(sa.ParentOptions).Any()));

            var attributes = selectedSubAttributes.Where(ca => ca.Type == CategoryAttributeType.MultiSelect || ca.Type == CategoryAttributeType.SingleSelect).ToList();

            var model = new ExpertCategoryAttributesModel
            {
                CategoryAttributes = attributes,
                CategoryAttributeValues = attributeValues
            };

            return PartialView(MVC.Account.Views._ExpertCategoryAttributes, model);
        }

        public virtual ActionResult ExpertSignUp(string recommendationKey = null)
        {
            if (AuthenticationHelper.IsAuthenticated)
                return RedirectToAction(MVC.Account.BecomeExpert());

            if(recommendationKey != null)
            {
                try
                {
                    var recommendation = Repository.Recommendation.CheckRecommendationKey(recommendationKey);

                    var categoryIds = new List<int>();
                    categoryIds.Add(recommendation.RecommendedCategory.Id);

                    var model = new ExpertSignUpModel(Repository.Category.All())
                    {
                        ExpertProfileForm = new ExpertProfileForm
                        {
                            FirstName = recommendation.RecommendedExpertName,
                            LastName = recommendation.RecommendedExpertSurname,
                            RecommendationId = recommendation.Id,
                            ExpertProfileCategoriesForm = new ExpertProfileCategoriesForm
                            {
                                SelectedCategories = categoryIds
                            }
                        },
                        UserProfileForm = new ProfileForm
                        {
                            EmailForm = new EmailForm
                            {
                                Email = recommendation.RecommendedExpertEmail
                            }
                        }
                    };

                    return View(model);
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
            return View(new ExpertSignUpModel(Repository.Category.All()));
        }

        [HttpPost]
        public virtual ActionResult ExpertSignUp(ExpertSignUpModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.ExpertProfileForm.RecommendationId != null)
                {
                    var recommendation = Repository.Recommendation.Get(model.ExpertProfileForm.RecommendationId ?? 0);

                    try
                    {
                        Repository.Recommendation.ConfirmExpertRegistration(recommendation.RecommendationKey);
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

                var user = Mapper.Map<ProfileForm, User>(model.UserProfileForm);
                var expert = Mapper.Map<ExpertProfileForm, Expert>(model.ExpertProfileForm);

                expert.PublicName = expert.FirstName + " " + expert.LastName;
                Repository.Expert.AssignProvisionToExpert(expert, ProvisionExpert.Novice);
                
                Repository.User.Add(user);

                Repository.User.AddEmailConfigurationDefaultValue(user);

                expert.User = user;

                if (model.ExpertProfileForm.RecommendationId != null)
                {
                    var recommendation = Repository.Recommendation.Get(model.ExpertProfileForm.RecommendationId ?? 0);
                    expert.Recommendation = recommendation;
                }

                Repository.Expert.Add(expert);

                CreatePartner(user);

                Email.Send<ActivationEmail>(user);
                Email.Send<NewExpertEmail>(expert.User);
                if(model.ExpertProfileForm.RecommendationId != null)
                {
                    var recommendation = Repository.Recommendation.Get(model.ExpertProfileForm.RecommendationId ?? 0);
                    if(recommendation.Recommender == null)
                    {
                        Email.Send<RecommenderNotRegisteredEmail>(recommendation);
                    }
                    else
                    {
                        Email.Send<RecommenderEmail>(recommendation.Recommender);
                    }
                }
                Log.Event<ExpertRegisteredEvent>(user);

                Flash.Success(Resources.Account.AccountCreated);

                return RedirectToAction(MVC.Account.SignIn());
            }

            return View(new ExpertSignUpModel(Repository.Category.All()){ UserProfileForm = model.UserProfileForm,ExpertProfileForm = model.ExpertProfileForm});
        }

        [AuthorizeSignedUpUser]
        public virtual ActionResult BecomeExpert()
        {
            if (AuthenticationHelper.IsExpert)
            {
                Flash.Info(Resources.Account.AlreadyExpert);
                return RedirectToAction(MVC.StaticPages.Home());
            }

            return View(new ExpertProfileFormModel(Repository.Category.All()));
        }

        [HttpPost]
        [AuthorizeSignedUpUser]
        public virtual ActionResult BecomeExpert([Bind(Prefix = "ExpertProfileForm")] ExpertProfileForm model)
        {
            if (!ModelState.IsValid)
                return View(new ExpertProfileFormModel(Repository.Category.All()) {ExpertProfileForm = model});

            if (!AuthenticationHelper.CurrentUser.IsExpert)
            {
                var expert = Mapper.Map<Expert>(model);

                expert.PublicName = expert.FirstName + " " + expert.LastName;
                Repository.Expert.AssignProvisionToExpert(expert, ProvisionExpert.Novice);

                Repository.Expert.Add(expert);

                CreatePartner(expert.User);

                Email.Send<NewExpertEmail>(expert.User);
            }

            Flash.Success(Resources.Account.ExpertConfirmation);
            Log.Event<ExpertRegisteredEvent>(AuthenticationHelper.CurrentUser);

            return RedirectToAction(MVC.Account.ExpertCategoryAttributes());
        }

        [AuthorizeSignedUpUser]
        public virtual ActionResult BecomePartner()
        {
            if (AuthenticationHelper.IsPartner)
            {
                Flash.Info(Resources.Account.AlreadyPartner);
                return RedirectToAction(MVC.StaticPages.Home());
            }

            Log.Event<BecomePartnerRequestEvent>(AuthenticationHelper.CurrentUser);

            Flash.Info(Resources.Account.PartnerConfirmation);
            return RedirectToAction(MVC.Profile.MyQuestions());
        }

        public virtual ActionResult Activate(string activationKey)
        {
            try
            {
                var user = Repository.User.Activate(activationKey);
                Log.Event<UserAccountActivatedEvent>(user);
                Flash.Success(Resources.Account.ActivationSuccessful);
            }
            catch (UserNotFoundException)
            {
                Flash.Error(Resources.Account.UserNotFound);
                return RedirectToAction(MVC.StaticPages.Home());
            }
            catch (UserAlreadyActivatedException)
            {
                Flash.Warning(Resources.Account.AlreadyActivated);
            }

            return RedirectToAction(MVC.Account.SignIn());
        }

        public virtual ActionResult PasswordForgotten()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult PasswordForgotten(EmailNotUniqueForm model)
        {
            if (ModelState.IsValid)
            {
                var user = Repository.User.FindByEmail(model.Email);

                if (user == null)
                {
                    Flash.Error(Resources.Account.EmailIncorrect);
                }
                else
                {
                    user.GenerateResetKey();
                    Repository.User.Update(user);

                    Email.Send<PasswordForgottenEmail>(user);

                    Flash.Info(Resources.Account.PasswordForgottenInstructions);
                    return RedirectToAction(MVC.StaticPages.Home());
                }
            }

            return View(model);
        }

        public virtual ActionResult ResendActivationEmail()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult ResendActivationEmail(EmailNotUniqueForm model)
        {
            if (ModelState.IsValid)
            {
                var user = Repository.User.FindByEmail(model.Email);

                if (user == null)
                {
                    Flash.Error(Resources.Account.EmailIncorrect);
                }
                else
                {
                    Email.Send<ActivationEmail>(user);
                    Repository.User.Update(user);

                    Flash.Info(Resources.Account.ActivationEmailResent);
                    return RedirectToAction(MVC.Account.SignIn());
                }
            }
            return View(model);
        }

        public virtual ActionResult ResetPassword(string resetKey)
        {
            try
            {
                Repository.User.FindByResetKey(resetKey);
                return View();
            }
            catch (UserNotFoundException)
            {
                Flash.Error(Resources.Account.UserNotFound);
                return RedirectToAction(MVC.StaticPages.Home());
            }
        }

        [HttpPost]
        public virtual ActionResult ResetPassword(string resetKey, PasswordForm model)
        {
            if (ModelState.IsValid)
            {
                var user = Repository.User.FindByResetKey(resetKey);
                Repository.User.ChangePassword(user, model.Password);

                if(user.IsNoSignUpUser)
                {
                    user.IsNoSignUpUser = false;
                    user.IsActivated = true;
                    Repository.User.Update(user);
                }

                Flash.Success(Resources.Account.PasswordChanged);
                return RedirectToAction(MVC.Account.SignIn());
            }

            return View(model);
        }

        private void CreatePartner(User user)
        {
            if (!user.IsPartner)
                Repository.Partner.Create(user);
        }
    }
}
