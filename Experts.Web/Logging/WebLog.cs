using Experts.Core.Logging;
using Experts.Core.Logging.Log4NetLog;
using Experts.Web.Helpers;

namespace Experts.Web.Logging
{
    public class WebLog : Log4NetLog
    {
        private readonly IHttpContextHelper _httpContextHelper;

        public WebLog(string applicationName)
            : base(applicationName)
        {
            _httpContextHelper = new HttpContextHelper();
            log4net.GlobalContext.Properties["SessionId"] = _httpContextHelper.SessionId;
            log4net.GlobalContext.Properties["RequestUrl"] = _httpContextHelper.RequestUrl;
        }
         
        public ILog ForRequestUrl(string requestUrl)
        {
            log4net.GlobalContext.Properties["RequestUrl"] = requestUrl;
            return this;
        }
    }
}