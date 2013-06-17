using System.Web.Mvc;
using Experts.Web.Filters;
using Experts.Web.Logging;

namespace Experts.Web.Controllers
{
    public partial class LogController : BaseController
    {
        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult Debug(WebLogEntity webLogEntity)
        {
            webLogEntity.SessionId = HttpContextHelper.SessionId;
            Log.Debug(GetSource(webLogEntity), webLogEntity.ToString());
            return new EmptyResult();
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult Info(WebLogEntity webLogEntity)
        {
            webLogEntity.SessionId = HttpContextHelper.SessionId;
            Log.Info(GetSource(webLogEntity), webLogEntity.ToString());
            return new EmptyResult();
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult Warn(WebLogEntity webLogEntity)
        {
            webLogEntity.SessionId = HttpContextHelper.SessionId;
            Log.Warn(GetSource(webLogEntity), webLogEntity.ToString());
            return new EmptyResult();
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult Error(WebLogEntity webLogEntity)
        {
            webLogEntity.SessionId = HttpContextHelper.SessionId;
            Log.Error(GetSource(webLogEntity), webLogEntity.ToString());
            return new EmptyResult();
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult Fatal(WebLogEntity webLogEntity)
        {
            webLogEntity.SessionId = HttpContextHelper.SessionId;
            Log.Fatal(GetSource(webLogEntity), webLogEntity.ToString());
            return new EmptyResult();
        }

        private string GetSource(WebLogEntity webLogEntity)
        {
            return GetType().FullName + ":" + webLogEntity.Source;
        }
    }
}