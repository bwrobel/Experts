using System.Web.Mvc;

namespace Experts.Web.Filters
{
    public class AssignMetadataAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var controllerName = filterContext.RouteData.GetRequiredString("Controller");
            var actionName = filterContext.RouteData.GetRequiredString("Action");

            var viewBag = filterContext.Controller.ViewBag;


            if (viewBag.PageTitle == null)
            {
                var title = Utils.AutoMetadata.GetTitle(controllerName, actionName);
                viewBag.PageTitle = MvcHtmlString.Create(title);

                if (title != null)
                    viewBag.RawPageTitle = title.Replace("<span>", string.Empty).Replace("</span>", string.Empty);
            }
            else
                viewBag.RawPageTitle = viewBag.PageTitle;


            if (viewBag.PageDescription == null)
                viewBag.PageDescription = Utils.AutoMetadata.GetDescription(controllerName, actionName);


            base.OnResultExecuting(filterContext);
        }


    }
}