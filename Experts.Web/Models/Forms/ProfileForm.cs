using System.ComponentModel.DataAnnotations;
using Experts.Web.Models.Account;
using Experts.Web.Validation;

namespace Experts.Web.Models.Forms
{
    public class ProfileForm
    {
        public EmailForm EmailForm { get; set; }

        [CheckBoxValidation(ErrorMessageResourceName = Resources.AccountConstants.PolicyUnchecked, ErrorMessageResourceType = typeof(Resources.Account))]
        [Display(Name = Resources.AccountConstants.PolicyPrivateTitle, ResourceType = typeof(Resources.Account))]
        public bool Policy { get; set; }

        public PasswordForm PasswordForm { get; set; }

        public MailConfigurationForm MailConfigurationForm { get; set; }

        public ExpertProfileFormModel ExpertProfileFormModel { get; set; }

        public PublicNameForm ModeratorPublicNameForm { get; set; }
    }
}