using System.Web;

namespace Experts.Web.Helpers
{
    public interface IHttpContextHelper
    {
        string SessionId { get; }
        string RequestUrl { get; }
    }

    public class HttpContextHelper : IHttpContextHelper
    {
        public string SessionId
        {
            get
            {
                return HttpContext.Current != null && HttpContext.Current.Session != null
                    ? HttpContext.Current.Session.SessionID ?? string.Empty
                    : string.Empty;
            }
        }

        public string RequestUrl
        {
            get
            {
                return HttpContext.Current != null
                    ? HttpContext.Current.Request.Url.AbsolutePath
                    : string.Empty;
            }
        }
    }
}