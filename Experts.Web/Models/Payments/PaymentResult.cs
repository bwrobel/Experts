using System.Web.Mvc;

namespace Experts.Web.Models.Payments
{
    public class PaymentResult
    {
        public int PaymentId { get; set; }
        public string ProviderPaymentId { get; set; }
        public ActionResult ResponseResult { get; set; }

        public bool IsSuccessfull { get; set; }
    }
}