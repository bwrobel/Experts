using System;
using System.Web.Mvc;

namespace Experts.Web.Helpers
{
    public static class NavigationHelper
    {
        public static MvcHtmlString Backlink(this HtmlHelper html, string linkName)
        {
            return MvcHtmlString.Create(html.ViewContext.RequestContext.HttpContext.Request.UrlReferrer != null
                ? String.Format(@"<a href=""{0}"">&lArr; {1}</a>", html.Encode(html.ViewContext.RequestContext.HttpContext.Request.UrlReferrer), linkName)
                : string.Empty);
        }
    }
}
