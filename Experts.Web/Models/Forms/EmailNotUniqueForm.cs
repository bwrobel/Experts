using System.ComponentModel.DataAnnotations;
using Experts.Web.Validation;

namespace Experts.Web.Models.Forms
{
    public class EmailNotUniqueForm
    {
        [Required(ErrorMessageResourceName = Resources.AccountConstants.EmailRequired, ErrorMessageResourceType = typeof(Resources.Account))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = Resources.AccountConstants.EmailIncorrect, ErrorMessageResourceType = typeof(Resources.Account))]
        [EmailValidation]
        [Display(Name = Resources.AccountConstants.Email, ResourceType = typeof(Resources.Account))]
        public string Email { get; set; }
    }
}