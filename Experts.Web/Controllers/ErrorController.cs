using System.Web.Mvc;
using Experts.Core.Entities.Events;
using Experts.Web.Helpers;
using Experts.Web.Models.Forms;

namespace Experts.Web.Controllers
{
    public partial class ErrorController : BaseController
    {
        public virtual ActionResult Error500(int eventId)
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
    }
}
