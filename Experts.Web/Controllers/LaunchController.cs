using System.Web.Mvc;
using Experts.Core.Entities;

namespace Experts.Web.Controllers
{
    public partial class LaunchController : BaseController
    {
        public virtual ActionResult Welcome()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult UserSubscribe(string email)
        {
            var subscription = new Subscription {Contact = email};
            Repository.Subscription.Add(subscription);

            return View();
        }

        [HttpPost]
        public virtual ActionResult ExpertSubscribe(string contact)
        {
            var subscription = new Subscription {Contact = contact, IsExpert = true};
            Repository.Subscription.Add(subscription);

            return View();
        }
    }
}
