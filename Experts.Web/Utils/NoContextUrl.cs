using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Experts.Web.Utils
{
    public static class NoContextUrl
    {
        private static string _baseUrl;
        public static string BaseUrl
        {
            get
            {
                if (_baseUrl == null)
                    _baseUrl = ConfigurationManager.AppSettings["BaseUrl"];

                return _baseUrl;
            }
        }

        public static string ActionAbsolute(ActionResult result)
        {
                var request = new HttpRequest("/", BaseUrl, "");
                var response = new HttpResponse(new StringWriter());
                var httpContext = new HttpContext(request, response);

                return new UrlHelper(httpContext.Request.RequestContext).ActionAbsolute(result);
        }
    }
}
