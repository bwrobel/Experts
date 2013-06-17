using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Experts.Core.Entities;
using Experts.Core.Repositories;
using Experts.Core.Utils;
using Experts.Web.Helpers;
using Experts.Web.Logging;

namespace Experts.Web.Controllers
{
    [HandleError]
    [SessionState(SessionStateBehavior.Required)]
    public partial class BaseController : Controller
    {
        protected WebLog Log;
        protected IHttpContextHelper HttpContextHelper;
        protected SEOKeyword RequestSeoKeyword;


        public BaseController()
        {
            HttpContextHelper = new HttpContextHelper();
            
        }

        public RepositoryFactory Repository
        {
            get { return RepositoryHelper.Repository; }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log = WebLogFactory.CreateNew();

            //Log.Debug(GetType(), "OnActionExecuting");

            if (!Request.Url.AbsoluteUri.StartsWith(ConfigurationManager.AppSettings["BaseUrl"]))
            {
                filterContext.Result = new RedirectResult(ConfigurationManager.AppSettings["BaseUrl"] + Request.Url.PathAndQuery, true);
                return;
            }

            base.OnActionExecuting(filterContext);

            ActiveUsersHelper.SetCurrentUserActive();

            var isPreLaunchMode = bool.Parse(ConfigurationManager.AppSettings["PreLaunchMode"]);

            var referrer = filterContext.RequestContext.HttpContext.Request.UrlReferrer;
            if (!isPreLaunchMode && referrer != null && referrer.Host.ToLower().Contains("google"))
            {
                var parameters = HttpUtility.ParseQueryString(referrer.Query);
                if (parameters.AllKeys.Contains("q"))
                {
                    var keywordPhrase = parameters["q"];

                    var currentKeyword = Repository.SEOKeyword.Find(query: sk => sk.ByPhrase(keywordPhrase)).SingleOrDefault();
                    if(currentKeyword != null)
                    {
                        currentKeyword.HitCount++;
                        Repository.SEOKeyword.Update(currentKeyword);

                        Log.Info(GetType(), "Keyword '{0}' hit no {1} from url '{2}'", currentKeyword.Phrase, currentKeyword.HitCount, HttpContextHelper.RequestUrl);
                    }
                    else
                    {
                        var keyword = new SEOKeyword { Phrase = keywordPhrase };
                        Repository.SEOKeyword.Add(keyword);

                        RequestSeoKeyword = keyword;

                        Log.Info(GetType(), "New Keyword {0}", keyword.Phrase);
                    }
                }
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Log.Debug(GetType(), "OnActionExecuted");

            BrokerHelper.SetNoBrokerCookie(filterContext);
            Response.Headers["Access-Control-Allow-Credentials"] = "true";
        }
    }
}
