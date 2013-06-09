using System.ComponentModel.DataAnnotations;
using Experts.Web.Validation;

namespace Experts.Web.Models.Forms
{
    public class EmailForm
    {
        [Required(ErrorMessageResourceName = Resources.AccountConstants.EmailRequired, ErrorMessageResourceType = typeof(Resources.Account))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = Resources.AccountConstants.EmailIncorrect, ErrorMessageResourceType = typeof(Resources.Account))]
        [Display(Name = Resources.AccountConstants.Email, ResourceType = typeof(Resources.Account))]
        [EmailValidation]
        [UserEmailAvailableForSignUp]
        [AutoFocus]
        public string Email { get; set; }
    }
}