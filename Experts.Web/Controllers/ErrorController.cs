using System.Linq;
using System.Web.Mvc;
using Experts.Core.Entities.Events;
using Experts.Web.Filters;
using Experts.Web.Helpers;
using Experts.Web.Models.Forms;

namespace Experts.Web.Controllers
{
    public partial class ErrorController : BaseController
    {
        [AssignMetadata]
        public virtual ActionResult Error500(int eventId = 0)
        {
            var model = new ApplicationErrorForm {EventId = eventId};
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Error500(ApplicationErrorForm form)
        {
            var logEvent = Repository.Event.Get(form.EventId);
            var newEvent = new UserFailureEvent();

            if (logEvent != null)
            {
                newEvent.Data = string.Format(Resources.Global.ErrorFormMessageTemplate, form.Description, form.Email) + logEvent.Data;
                Repository.Event.Add(newEvent);
                Repository.Event.Delete(logEvent);
            }

            Flash.Info(Resources.Global.ErrorFormConfirmation);
            return RedirectToAction(MVC.StaticPages.Home());
        }

        [AssignMetadata]
        public virtual ActionResult Error404()
        {
            ViewBag.RequestedUrl = Request.Url.OriginalString.Split(new[] { ';' }).Last();
            return View();
        }
    }
}
