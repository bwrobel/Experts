using System.Web.Mvc;
using Experts.Core.Logging;
using Experts.Core.Logging.Log4NetLog;
using Experts.Web.Filters;
using Experts.Web.Helpers;
using Experts.Web.Logging;

namespace Experts.Web.Controllers
{
    public partial class LogController : BaseController
    {
        private readonly SessionHelper _sessionHelper;
        private readonly ILog _log;

        public LogController()
        {
            _sessionHelper = new SessionHelper();
            _log = Log4NetLogFactory.CreateNew();
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult Debug(WebLog webLog)
        {
            webLog.SessionId = _sessionHelper.CurrentSession;
            LogWithSource(webLog).Debug(webLog.ToString());
            return new EmptyResult();
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult Info(WebLog webLog)
        {
            webLog.SessionId = _sessionHelper.CurrentSession;
            LogWithSource(webLog).Info(webLog.ToString());
            return new EmptyResult();
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult Warn(WebLog webLog)
        {
            webLog.SessionId = _sessionHelper.CurrentSession;
            LogWithSource(webLog).Warn(webLog.ToString());
            return new EmptyResult();
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult Error(WebLog webLog)
        {
            webLog.SessionId = _sessionHelper.CurrentSession;
            LogWithSource(webLog).Error(webLog.ToString());
            return new EmptyResult();
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult Fatal(WebLog webLog)
        {
            webLog.SessionId = _sessionHelper.CurrentSession;
            LogWithSource(webLog).Fatal(webLog.ToString());
            return new EmptyResult();
        }

        private ILog LogWithSource(WebLog webLog)
        {
            return _log.SetSource(GetType() + ":" + webLog.Source);
        }
    }
}