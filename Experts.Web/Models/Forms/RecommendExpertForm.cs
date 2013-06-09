using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Experts.Web.Validation;

namespace Experts.Web.Models.Forms
{
    public class RecommendExpertForm
    {
        [HiddenInput(DisplayValue = false)]
        public int? RecommenderId { get; set; }

        [Display(Name = Resources.AccountConstants.RecommenderEmail, ResourceType = typeof(Resources.Account))]
        [Required(ErrorMessageResourceName = Resources.AccountConstants.EmailRequired, ErrorMessageResourceType = typeof(Resources.Account))]
        public string RecommenderEmail { get; set; }

        [Display(Name = Resources.AccountConstants.RecommenderName, ResourceType = typeof(Resources.Account))]
        [Required(ErrorMessageResourceName = Resources.AccountConstants.FirstNameRequired, ErrorMessageResourceType = typeof(Resources.Account))]
        public string RecommenderName { get; set; }

        [Display(Name = Resources.AccountConstants.RecommenderSurname, ResourceType = typeof(Resources.Account))]
        [Required(ErrorMessageResourceName = Resources.AccountConstants.LastNameRequired, ErrorMessageResourceType = typeof(Resources.Account))]
        public string RecommenderSurname { get; set; }

        [Display(Name = Resources.AccountConstants.RecommendedExpertEmail, ResourceType = typeof(Resources.Account))]
        [Required(ErrorMessageResourceName = Resources.AccountConstants.EmailRequired, ErrorMessageResourceType = typeof(Resources.Account))]
        [EmailValidation]
        [UserEmailAvailableForSignUp]
        [AutoFocus]
        public string RecommendedExpertEmail { get; set; }

        [Display(Name = Resources.AccountConstants.RecommendedExpertName, ResourceType = typeof(Resources.Account))]
        [Required(ErrorMessageResourceName = Resources.AccountConstants.FirstNameRequired, ErrorMessageResourceType = typeof(Resources.Account))]
        public string RecommendedExpertName { get; set; }

        [Display(Name = Resources.AccountConstants.RecommendedExpertSurname, ResourceType = typeof(Resources.Account))]
        [Required(ErrorMessageResourceName = Resources.AccountConstants.LastNameRequired, ErrorMessageResourceType = typeof(Resources.Account))]
        public string RecommendedExpertSurname { get; set; }

        [Display(Name = Resources.AccountConstants.RecommendedExpertCategory, ResourceType = typeof(Resources.Account))]
        [Required(ErrorMessageResourceName = Resources.AccountConstants.CategoryRequired, ErrorMessageResourceType = typeof(Resources.Account))]
        public int RecommendedExpertCategoryId { get; set; }
        //
        public IEnumerable<SelectListItem> Categories { get; set; }

        [Display(Name = Resources.AccountConstants.RecommendationEmailMessage, ResourceType = typeof(Resources.Account))]
        [DataType(DataType.MultilineText)]
        public string RecommendedExpertComment { get; set; }
    }
}