using System.Web.Mvc;
using Experts.Core.Entities;

namespace Experts.Web.Models.Payments
{
    public class PaymentConfirmationModel
    {
        public ActionResult RedirectResult { get; set; }
        public PaymentStatus Status { get; set; }
    }
}