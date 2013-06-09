using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Experts.Web.Models.Forms
{

    public class PasswordForm
    {
        [Required(ErrorMessageResourceName = Resources.AccountConstants.PasswordRequired, ErrorMessageResourceType = typeof(Resources.Account))]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceName = Resources.AccountConstants.PasswordTooShort, ErrorMessageResourceType = typeof(Resources.Account))]
        [Display(Name = Resources.AccountConstants.Password, ResourceType = typeof(Resources.Account))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceName = Resources.AccountConstants.PasswordMismatch, ErrorMessageResourceType = typeof(Resources.Account))]
        [Display(Name = Resources.AccountConstants.PasswordConfirmation, ResourceType = typeof(Resources.Account))]
        public string PasswordConfirmation { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = Resources.AccountConstants.OldPassword, ResourceType = typeof(Resources.Account))]
        public string OldPassword { get; set; }
    }
}