using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Experts.Web.Models.Forms;
using Experts.Web.Validation;

namespace Experts.Web.Models.Payments
{
    public class PaymentForm
    {
        [ScaffoldColumn(false)]
        public PaymentFormPersonalData PersonalData { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = Resources.PaymentConstants.PaymentFormEmail, ResourceType = typeof(Resources.Payment))]
        public bool SignUp { get; set; }

        [ScaffoldColumn(false)]
        public PasswordForm PasswordForm { get; set; }

        [ScaffoldColumn(false)]
        [CheckBoxValidation(ErrorMessageResourceName = Resources.AccountConstants.PolicyUnchecked, ErrorMessageResourceType = typeof(Resources.Account))]
        [Display(Name = Resources.AccountConstants.PolicyPrivateTitle, ResourceType = typeof(Resources.Account))]
        public bool Policy { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int StrategyId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int RelatedId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int SurchargeId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int SelectedChannel { get; set; }
    }

    public class PaymentFormPersonalData
    {
        [Required(ErrorMessageResourceName = Resources.PaymentConstants.PaymentFormFieldFirstNameRequired, ErrorMessageResourceType = typeof(Resources.Payment))]
        [Display(Name = Resources.PaymentConstants.PaymentFormFirstName, ResourceType = typeof(Resources.Payment))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = Resources.PaymentConstants.PaymentFormFieldLastNameRequired, ErrorMessageResourceType = typeof(Resources.Payment))]
        [Display(Name = Resources.PaymentConstants.PaymentFormLastName, ResourceType = typeof(Resources.Payment))]
        public string LastName { get; set; }

        [EmailValidation]
        [Required(ErrorMessageResourceName = Resources.PaymentConstants.PaymentFormFieldEmailRequired, ErrorMessageResourceType = typeof(Resources.Payment))]
        [EmailUniqueIfAnonymous]
        [Display(Name = Resources.PaymentConstants.PaymentFormEmail, ResourceType = typeof(Resources.Payment))]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}