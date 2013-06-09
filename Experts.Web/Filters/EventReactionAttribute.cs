using System;
using System.Linq;
using System.Web.Mvc;
using Experts.Web.Controllers;
using Experts.Web.Helpers;
using Experts.Web.Models.Events;

namespace Experts.Web.Filters
{
    public class EventReactionAttribute : ActionFilterAttribute
    {
        public int? EventId { get; set; }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);

            if (EventId == null)
                return;

            var controller = (BaseController)filterContext.Controller;

            var systemEvent = controller.Repository.Event.Get(EventId.Value);

            systemEvent.IsHandled = true;
            systemEvent.HandledBy = AuthenticationHelper.CurrentUser;

            controller.Repository.Event.Update(systemEvent);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var eventReactionModel = filterContext.ActionParameters.Select(ap => ap.Value)
                    .SingleOrDefault(v => v.GetType().BaseType != null && v.GetType().BaseType.IsGenericType && v.GetType().BaseType.GetGenericTypeDefinition() == typeof(EventReactionModel<>));

            if (eventReactionModel == null)
                throw new ArgumentException("One of action parameters has to inherit from EventReactionModel");

            EventId = (int)eventReactionModel.GetType().GetProperty("EventId").GetValue(eventReactionModel, null);

            if(EventId <= 0)
                throw new ArgumentException("EventId has to be a real event identifier value");

            base.OnActionExecuting(filterContext);
        }
    }
}