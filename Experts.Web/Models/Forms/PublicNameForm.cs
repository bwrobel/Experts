using System.ComponentModel.DataAnnotations;

namespace Experts.Web.Models.Forms
{
    public class PhoneNumberForm
    {
        [Required(ErrorMessageResourceName = Resources.AccountConstants.PhoneNumberRequired, ErrorMessageResourceType = typeof(Resources.Account))]
        [StringLength(20, MinimumLength = 9, ErrorMessageResourceName = Resources.AccountConstants.PhoneNumberIncorrect, ErrorMessageResourceType = typeof(Resources.Account))]
        [Display(Name = Resources.AccountConstants.PhoneNumber, ResourceType = typeof(Resources.Account))]
        public string PhoneNumber { get; set; }
    }
}