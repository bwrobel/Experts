using System.Web.Mvc;
using Experts.Core.Entities;
using Experts.Web.Filters;

namespace Experts.Web.Controllers
{
    public partial class LaunchController : BaseController
    {
        [AssignMetadata]
        public virtual ActionResult Welcome()
        {
            return View();
        }

        [AssignMetadata]
        [HttpPost]
        public virtual ActionResult UserSubscribe(string email)
        {
            var subscription = new Subscription {Contact = email};
            Repository.Subscription.Add(subscription);

            return View();
        }

        [AssignMetadata]
        [HttpPost]
        public virtual ActionResult ExpertSubscribe(string contact)
        {
            var subscription = new Subscription {Contact = contact, IsExpert = true};
            Repository.Subscription.Add(subscription);

            return View();
        }
    }
}
