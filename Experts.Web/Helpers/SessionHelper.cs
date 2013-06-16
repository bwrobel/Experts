using System.Web;

namespace Experts.Web.Helpers
{
    public interface ISessionHelper
    {
        string CurrentSession { get; }
    }

    public class SessionHelper : ISessionHelper
    {
        public string CurrentSession
        {
            get
            {
                return HttpContext.Current != null && HttpContext.Current.Session != null
                    ? HttpContext.Current.Session.SessionID ?? string.Empty
                    : string.Empty;
            }
        }
    }
}