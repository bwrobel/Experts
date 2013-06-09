using System.ComponentModel.DataAnnotations;

namespace Experts.Web.Models.Forms
{
    public class PayoffForm
    {
        [Display(Name = Resources.AccountConstants.TransferValue, ResourceType = typeof(Resources.Account))]
        [Required(ErrorMessageResourceName = Resources.AccountConstants.TransferValueRequired, ErrorMessageResourceType = typeof(Resources.Account))]
        public decimal? PayoffValue { get; set; }

        [Display(Name = Resources.AccountConstants.BankAccountNumber, ResourceType = typeof(Resources.Account))]
        [Required(ErrorMessageResourceName = Resources.AccountConstants.BankAccountNumberRequired, ErrorMessageResourceType = typeof(Resources.Account))]
        public string BankAccount { get; set; }
    }
}