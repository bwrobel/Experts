using Experts.Web.Models.Payments.Providers;

namespace Experts.Web.Models.Payments
{
    public class PaymentFormModel
    {
        public PaymentForm PaymentForm { get; set; }
        public PaymentProvider Provider { get; set; }
        public bool HasEnoughFunds { get; set; }
        public decimal? Funds { get; set; }
    }
}