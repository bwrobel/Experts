using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace Experts.Web.Helpers
{
    public static class BrokerHelper
    {
        private const string BrokerCookieName = "broker";
        private const int NoBrokerCookieValue = -1;

        private static readonly string[] NoBrokerCookieExcludedActions = new[] { MVC.Profile.ActionNames.ExpertWidget, "PageNotFound" };

        public static void SetBrokerCookie(int brokerId)
        {
            var cookieBrokerId = GetBrokerIdFromCookie();
            if (cookieBrokerId == null || cookieBrokerId.Value != NoBrokerCookieValue)
                SetCookie(brokerId);
        }

        public static int? GetBrokerIdFromCookie()
        {
            var cookie = HttpContext.Current.Request.Cookies.Get(BrokerCookieName);
            if(cookie == null) return null;

            int cookieValue;
            if (!int.TryParse(cookie.Value, out cookieValue)) return null;

            return cookieValue;
        }

        public static void SetNoBrokerCookie(ActionExecutedContext filterContext)
        {
            if (NoBrokerCookieExcludedActions.Contains(filterContext.ActionDescriptor.ActionName))
                return;

            if (GetBrokerIdFromCookie() == null)
                SetCookie(NoBrokerCookieValue);
        }

        private static void SetCookie(int value)
        {
            var cookie = new HttpCookie(BrokerCookieName, value.ToString());
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}