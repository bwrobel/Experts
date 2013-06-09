using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Experts.Core.Repositories;
using Experts.Web.Binders;
using Experts.Web.Controllers;
using Experts.Web.Jobs;
using Experts.Web.Utils;
using Experts.Web.Helpers;

namespace Experts.Web
{
    public class MvcApplication : HttpApplication
    {
        private static bool IsPreLaunchModeEnabled
        {
            get { return bool.Parse(ConfigurationManager.AppSettings["PreLaunchMode"]); }
        }

        private static bool AreCustomErrorsEnabled
        {
            get { return bool.Parse(ConfigurationManager.AppSettings["CustomErrors"]); }
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            if (IsPreLaunchModeEnabled)
                routes.MapRoute( "LaunchHome", "", MVC.Launch.Welcome());

            AutoRouter.AutoRegisterRoutes(routes, IsPreLaunchModeEnabled ? typeof(LaunchController) : null);

            routes.MapRoute(
                "Catch-all",
                "{*catchall}",
                 MVC.StaticPages.PageNotFound() 
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());

            AutoMapperConfiguration.Configure();
            AutoMapper.Mapper.AssertConfigurationIsValid();

            if (!IsPreLaunchModeEnabled)
                Scheduler.Configure(HttpContext.Current);

            AutoMetadata.ValidateMetadataResources();
        }   

        protected void Application_End()
        {
            Scheduler.GetScheduler().Shutdown();
        }

        protected void Application_EndRequest()
        {
            var factory = HttpContext.Current.Items["RepositoryFactory"] as RepositoryFactory;

            if (factory != null)
                factory.Dispose();
        }

        void Application_Error()
        {
            if (!AreCustomErrorsEnabled)
                return;

            var exception = Server.GetLastError();
            var eventId = ErrorsHelper.LogApplicationException(exception);

            Response.Clear();
            Response.Redirect("~/blad-aplikacji/" + eventId);
        }
    }
}