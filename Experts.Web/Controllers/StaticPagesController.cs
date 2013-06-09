using System.Web.Mvc;
using Experts.Web.Filters;
using Experts.Web.Helpers;
using Experts.Web.Models.StaticPages;

namespace Experts.Web.Controllers
{
    public partial class StaticPagesController : BaseController
    {
        public virtual ActionResult Home()
        {
            return View(new HomeModel(Repository.Category.All()));
        }

        public virtual ActionResult AdCampaignHome(string code)
        {
            var landingPage = Repository.AdCampaignLandingPage.Get(code);

            if(landingPage == null)
                RedirectToActionPermanent(MVC.StaticPages.Home());

            return View(new AdCampaignHomeModel(Repository.Category.All(), landingPage));
        }

        [DefaultRouting]
        public virtual ActionResult TopMenu()
        {
            return PartialView(MVC.Shared.Views._TopMenu);
        }

        public virtual ActionResult MeetAsknuts(string anhor = null)
        {
            var anhorName = string.Empty;

            if (anhor == Resources.StaticPages.Terms)
                anhorName = "terms";

            else if (anhor == Resources.StaticPages.Faq)
                anhorName = "faq";

            else if (anhor == Resources.StaticPages.ExpertFaq)
                anhorName = "expertfaq";

            else if (anhor == Resources.StaticPages.Privacy)
                anhorName = "privacy";

            return View((object)anhorName);
        }

        [DefaultRouting]
        public virtual ActionResult PolicyPrivate()
        {
            return PartialView();
        }

        [DefaultRouting]
        public virtual ActionResult PolicyWeb()
        {
            return PartialView();
        }

        public virtual ActionResult PageNotFound()
        {
            return HttpNotFound();
        }

        [DefaultRouting]
        public virtual ActionResult CategoryDescription(int? categoryId = null)
        {
            if (!categoryId.HasValue)
                return Json(Resources.StaticPages.HomeSystemDescription, JsonRequestBehavior.AllowGet);

            var category = Repository.Category.Get(categoryId.Value);
            return Json(category.Description, JsonRequestBehavior.AllowGet);
        }

        [DefaultRouting]
        public virtual ActionResult About()
        {
            return PartialView();
        }

        //public virtual ActionResult ExpertFAQ()
        //{
        //    var model = new ExpertFAQModel
        //                    {
        //                        HowDoesItWork = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.HowDoesItWorkExpert_Title,
        //                            Description = Resources.Help.HowDoesItWorkExpert_Description
        //                        },
        //                        BecomingExpert = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.BecomingExpert_Title,
        //                            Description = Resources.Help.BecomingExpert_Description
        //                        },
        //                        Requirements = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.Requirements_Title,
        //                            Description = Resources.Help.Requirements_Description
        //                        },
        //                        Costs = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.Costs_Title,
        //                            Description = Resources.Help.Costs_Description
        //                        },
        //                        AdditionalAdvertisement = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.AdditionalAdvertisement_Title,
        //                            Description = Resources.Help.AdditionalAdvertisement_Description
        //                        },
        //                        PrivacyAndSecurity = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.PrivacyAndSecurityExpert_Title,
        //                            Description = Resources.Help.PrivacyAndSecurityExpert_Description
        //                        },
        //                        Advantages = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.Advantages_Title,
        //                            Description = Resources.Help.Advantages_Description
        //                        },
        //                        AffiliateProgram = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.AffiliateProgramExpert_Title,
        //                            Description = Resources.Help.AffiliateProgramExpert_Description
        //                        },
        //                        MaximumExpertsByCategory = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.MaximumExpertsByCategory_Title,
        //                            Description = Resources.Help.MaximumExpertsByCategory_Description
        //                        },
        //                        LatestEvents = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.LatestEvents_Title,
        //                            Description = Resources.Help.LatestEvents_Description
        //                        },
        //                        Certificate = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.Certificate_Title,
        //                            Description = Resources.Help.Certificate_Description
        //                        },
        //                        Verification = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.Verification_Title,
        //                            Description = Resources.Help.Verification_Description
        //                        },
        //                        LosingVerificationStatus = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.LosingVerificationStatus_Title,
        //                            Description = Resources.Help.LosingVerificationStatus_Description
        //                        },
        //                        LackOfCategory = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.LackOfCategory_Title,
        //                            Description = Resources.Help.LackOfCategory_Description
        //                        },
        //                        CategoryQuantity = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.CategoryQuantity_Title,
        //                            Description = Resources.Help.CategoryQuantity_Description
        //                        },
        //                        AvatarUpload = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.AvatarUpload_Title,
        //                            Description = Resources.Help.AvatarUpload_Description
        //                        },
        //                        Experience = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.Experience_Title,
        //                            Description = Resources.Help.Experience_Description
        //                        },
        //                        Degree = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.Degree_Title,
        //                            Description = Resources.Help.Degree_Description
        //                        },
        //                        EmailConfiguration = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.EmailConfiguration_Title,
        //                            Description = Resources.Help.EmailConfiguration_Description
        //                        },
        //                        ForgottenPassword = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.ForgottenPasswordExpert_Title,
        //                            Description = Resources.Help.ForgottenPasswordExpert_Description
        //                        },
        //                        AnsweringQuestions = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.AnsweringQuestions_Title,
        //                            Description = Resources.Help.AnsweringQuestions_Description
        //                        },
        //                        FindingQuestions = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.FindingQuestions_Title,
        //                            Description = Resources.Help.FindingQuestions_Description
        //                        },
        //                        AnsweringRightQuestions = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.AnsweringRightQuestions_Title,
        //                            Description = Resources.Help.AnsweringRightQuestions_Description
        //                        },
        //                        Chat = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.Chat_Title,
        //                            Description = Resources.Help.Chat_Description
        //                        },
        //                        QuestionBlocking = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.QuestionBlocking_Title,
        //                            Description = Resources.Help.QuestionBlocking_Description
        //                        },
        //                        OccupyingAndBlocking = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.OccupyingAndBlocking_Title,
        //                            Description = Resources.Help.OccupyingAndBlocking_Description
        //                        },
        //                        BlockingTime = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.BlockingTime_Title,
        //                            Description = Resources.Help.BlockingTime_Description
        //                        },
        //                        OccupyExpertLimit = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.OccupyExpertLimit_Title,
        //                            Description = Resources.Help.OccupyExpertLimit_Description
        //                        },
        //                        AnsweringEfficiently = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.AnsweringEfficiently_Title,
        //                            Description = Resources.Help.AnsweringEfficiently_Description
        //                        },
        //                        AnswerAcceptation = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.AnswerAcceptation_Title,
        //                            Description = Resources.Help.AnswerAcceptation_Description
        //                        },
        //                        ChangingQuestionValue = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.ChangingQuestionValue_Title,
        //                            Description = Resources.Help.ChangingQuestionValue_Description
        //                        },
        //                        UndervaluedQuestion = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.UndervaluedQuestion_Title,
        //                            Description = Resources.Help.UndervaluedQuestion_Description
        //                        },
        //                        SuggestMore = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.SuggestMore_Title,
        //                            Description = Resources.Help.SuggestMore_Description
        //                        },
        //                        AdditionalQuestionService = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.AdditionalQuestionService_Title,
        //                            Description = Resources.Help.AdditionalQuestionService_Description
        //                        },
        //                        Gratuity = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.GratuityExpert_Title,
        //                            Description = Resources.Help.GratuityExpert_Description
        //                        },
        //                        Evaluation = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.Evaluation_Title,
        //                            Description = Resources.Help.Evaluation_Description
        //                        },
        //                        AnsweringRules = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.AnsweringRules_Title,
        //                            Description = Resources.Help.AnsweringRules_Description
        //                        },
        //                        QuestionInWrongCategory = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.QuestionInWrongCategory_Title,
        //                            Description = Resources.Help.QuestionInWrongCategory_Description
        //                        },
        //                        Advertisement = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.Advertisement_Title,
        //                            Description = Resources.Help.Advertisement_Description
        //                        },
        //                        AdditionalWaysOfEarning = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.AdditionalWaysOfEarning_Title,
        //                            Description = Resources.Help.AdditionalWaysOfEarning_Description
        //                        },
        //                        OccupyLimit = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.OccupyExpertLimit_Title,
        //                            Description = Resources.Help.OccupyExpertLimit_Description
        //                        },
        //                        UserWontAcceptAnswer = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.UserWontAcceptAnswer_Title,
        //                            Description = Resources.Help.UserWontAcceptAnswer_Description
        //                        },
        //                        CopyPasteAnswer = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.CopyPasteAnswer_Title,
        //                            Description = Resources.Help.CopyPasteAnswer_Description
        //                        },
        //                        AnswerAndMoreDetails = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.AnswerAndMoreDetails_Title,
        //                            Description = Resources.Help.AnswerAndMoreDetails_Description
        //                        },
        //                        PersonalInformation = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.PersonalInformationExpert_Title,
        //                            Description = Resources.Help.PersonalInformationExpert_Description
        //                        },
        //                        GratuityHow = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.GratuityHow_Title,
        //                            Description = Resources.Help.GratuityHow_Description
        //                        },
        //                        AnsweringGain = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.AnsweringGain_Title,
        //                            Description = Resources.Help.AnsweringGain_Description
        //                        },
        //                        Balance = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.Balance_Title,
        //                            Description = Resources.Help.Balance_Description
        //                        },
        //                        Payments = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.Payments_Title,
        //                            Description = Resources.Help.Payments_Description
        //                        },
        //                        QuestionRebate = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.QuestionRebate_Title,
        //                            Description = Resources.Help.QuestionRebate_Description
        //                        },
        //                        MoreFAQ = new HelpQuestionModel
        //                        {
        //                            Title = Resources.Help.MoreFAQ_Title,
        //                            Description = Resources.Help.MoreFAQ_Description
        //                        },

        //                    };

        //    return PartialView(model);
        //}

        [DefaultRouting]
        public virtual ActionResult _HelpQuestionStandard()
        {
            return PartialView();
        }
    }
}
