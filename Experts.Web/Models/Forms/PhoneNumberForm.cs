using System.ComponentModel.DataAnnotations;
using Experts.Web.Validation;

namespace Experts.Web.Models.Forms
{
    public class PublicNameForm
    {
        [Required(ErrorMessageResourceName = Resources.AccountConstants.PublicNameRequired, ErrorMessageResourceType = typeof(Resources.Account))]
        [StringLength(25, MinimumLength = 2, ErrorMessageResourceName = Resources.AccountConstants.PublicNameTooLong, ErrorMessageResourceType = typeof(Resources.Account))]
        [Display(Name = Resources.AccountConstants.PublicName, ResourceType = typeof(Resources.Account))]
        [UserPublicNameUnique]
        [UserPublicNameProperCharacters]
        public string PublicName { get; set; }
    }
}