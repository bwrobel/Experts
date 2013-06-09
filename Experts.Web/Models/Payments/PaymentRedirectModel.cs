using System.Web.Mvc;
using Experts.Web.Models.Payments.Providers;

namespace Experts.Web.Models.Payments
{
    public class PaymentRedirectModel
    {
        public ActionResult ImmediateRedirectActionResult { get; set; }
        public int PaymentId { get; set; }
        public PaymentForm PaymentForm { get; set; }
        public PaymentProvider Provider { get; set; }
        public decimal Amount { get; set; }
    }
}