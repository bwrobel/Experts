using System.ComponentModel.DataAnnotations;
using Experts.Core.Entities;

namespace Experts.Web.Models.Forms
{
    public class ExpertProfileForm
    {
        [Required(ErrorMessageResourceName = Resources.AccountConstants.FirstNameRequired, ErrorMessageResourceType = typeof(Resources.Account))]
        [Display(Name = Resources.AccountConstants.FirstName, ResourceType = typeof(Resources.Account))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = Resources.AccountConstants.LastNameRequired, ErrorMessageResourceType = typeof(Resources.Account))]
        [Display(Name = Resources.AccountConstants.LastName, ResourceType = typeof(Resources.Account))]
        public string LastName { get; set; }

        public PhoneNumberForm PhoneNumberForm { get; set; }

        [Required(ErrorMessageResourceName = Resources.AccountConstants.SelectCategory, ErrorMessageResourceType = typeof(Resources.Account))]
        public ExpertProfileCategoriesForm ExpertProfileCategoriesForm { get; set; }

        public ExpertMicroprofileForm ExpertMicroprofileForm { get; set; }

        public PublicNameForm PublicNameForm { get; set; }

        public int? RecommendationId { get; set; }
    }
}