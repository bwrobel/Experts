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
            Log.ForRequestUrl(webLogEntity.Url).Debug(GetSource(webLogEntity), webLogEntity.Message);
            return new EmptyResult();
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult Info(WebLogEntity webLogEntity)
        {
            Log.ForRequestUrl(webLogEntity.Url).Info(GetSource(webLogEntity), webLogEntity.Message);
            return new EmptyResult();
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult Warn(WebLogEntity webLogEntity)
        {
            Log.ForRequestUrl(webLogEntity.Url).Warn(GetSource(webLogEntity), webLogEntity.Message);
            return new EmptyResult();
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult Error(WebLogEntity webLogEntity)
        {
            Log.ForRequestUrl(webLogEntity.Url).Error(GetSource(webLogEntity), webLogEntity.Message);
            return new EmptyResult();
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult Fatal(WebLogEntity webLogEntity)
        {
            Log.ForRequestUrl(webLogEntity.Url).Fatal(GetSource(webLogEntity), webLogEntity.Message);
            return new EmptyResult();
        }

        private string GetSource(WebLogEntity webLogEntity)
        {
            return GetType().FullName + ":" + webLogEntity.Source;
        }
    }
}