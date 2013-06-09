using System.Web.Mvc;
using System.Web.Routing;

namespace Experts.Web.Helpers
{
    public static class HtmlImageHelper
    {
        public static MvcHtmlString Image(this HtmlHelper html, string url, int? width = null, int? height = null, string alt = "", object htmlAttributes = null)
        {
            var img = new TagBuilder("img");
            
            img.MergeAttribute("src", url);
            if (width != null) img.MergeAttribute("width", width.ToString());
            if (height != null) img.MergeAttribute("height", height.ToString());
            img.MergeAttribute("alt", alt);
            img.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString ImageLink(this HtmlHelper html, RouteValueDictionary routValues, string url, int? width = null, int? height = null, string alt = "", object htmlAttributes = null)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            var linkUrl = urlHelper.RouteUrl(routValues);

            var linkTagBuilder = new TagBuilder("a");
            linkTagBuilder.MergeAttribute("href", linkUrl);
            linkTagBuilder.InnerHtml = html.Image(url, width, height, alt, htmlAttributes).ToString();

            return MvcHtmlString.Create(linkTagBuilder.ToString());
        }
    }
}