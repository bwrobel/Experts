using System.ComponentModel.DataAnnotations;
using Experts.Web.Validation;

namespace Experts.Web.Models.Forms
{
    public class SignInForm
    {
        [Required(ErrorMessageResourceName = Resources.AccountConstants.LoginRequired, ErrorMessageResourceType = typeof(Resources.Account))]
        [Display(Name = Resources.AccountConstants.Login, ResourceType = typeof(Resources.Account))]
        [AutoFocus]
        public string Login { get; set; }

        [Required(ErrorMessageResourceName = Resources.AccountConstants.PasswordRequired, ErrorMessageResourceType = typeof(Resources.Account))]
        [Display(Name = Resources.AccountConstants.Password, ResourceType = typeof(Resources.Account))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [ScaffoldColumn(false)]
        public bool ResendActivationMail { get; set; }
    }
}