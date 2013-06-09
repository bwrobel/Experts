using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.Routing;
using System.Linq;
using System.Web.Mvc;
using System.Threading;
using Experts.Web.Filters;

namespace Experts.Web.Utils
{
    public static class AutoRouter
    {
        public static void AutoRegisterRoutes(RouteCollection routes, Type forceController = null)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var controllers = forceController == null ? assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Controller)) && !t.Name.StartsWith("T4MVC_")) : new List<Type> { forceController };

            var currentRoutes = new Dictionary<string, bool>();
            var routeHandler = new MvcRouteHandler();

            foreach (var controller in controllers)
            {
                var controllerName = controller.Name.Replace("Controller", string.Empty);
                var actions = controller.GetMethods()
                       .Where(m => m.ReturnType.IsSubclassOf(typeof(ActionResult)) || m.ReturnType == typeof(ActionResult))
                       .Where(m => !m.GetCustomAttributes(typeof(NonActionAttribute), true).Any())
                       .Where(m => m.IsPublic);

                foreach (var action in actions)
                {
                    var isPartialAction = action.GetCustomAttributes(typeof (DefaultRoutingAttribute), true).Any();
                    string route;

                    if (!isPartialAction)
                    {
                        var resourceName = string.Format("{0}_{1}", controllerName, action.Name);
                        route = Resources.Routes.ResourceManager.GetString(resourceName);

                        if (!currentRoutes.ContainsKey(resourceName))
                            currentRoutes.Add(resourceName, route == null);

                        if (route == null)
                            continue;
                    }
                    else
                        route = string.Format("{0}/{1}", controllerName, action.Name);

                    var routeDictionary = new RouteValueDictionary
                                              {
                                                  {"controller", controllerName},
                                                  {"action", action.Name},
                                                  {"area", string.Empty}
                                              };    
                    if (!action.GetCustomAttributes(typeof(HttpPostAttribute), true).Any())
                    {  
                        var parameters = action.GetParameters();
                        var parametersString = new StringBuilder();
                        foreach (var parameter in parameters.Where(p => !p.GetCustomAttributes(typeof(QueryParameterAttribute), true).Any()))
                        {
                            if(!(parameter.DefaultValue is DBNull))
                                routeDictionary.Add(parameter.Name, UrlParameter.Optional);

                            parametersString.Append("/{" + parameter.Name + "}");
                        }

                        route += parametersString.ToString();
                    }

                    routes.Add(new Route(route, routeDictionary, routeHandler)); 
                }
            }

            var missingRoutes = currentRoutes.Where(kvp => kvp.Value).ToList();
            if (missingRoutes.Any())
            {
                var missingRoutesInfo = new StringBuilder();
                foreach (var missingRoute in missingRoutes)
                    missingRoutesInfo.AppendLine(missingRoute.Key);

                throw new Exception("Missing routes: " + Environment.NewLine + missingRoutesInfo);
            }

            var resourceSet = Resources.Routes.ResourceManager.GetResourceSet(Thread.CurrentThread.CurrentUICulture, false, false);
            var resourcesDictionary = resourceSet.Cast<DictionaryEntry>().ToDictionary(entry => (string) entry.Key, entry => (string) entry.Value);
            resourceSet.Close();

            if (forceController == null)
            {
                foreach (var entry in resourcesDictionary)
                {
                    if (!currentRoutes.ContainsKey(entry.Key))
                        throw new Exception("Unnecessary resource key: " + entry.Key);

                    if (resourcesDictionary.Count(kvp => entry.Key == kvp.Key) > 1)
                        throw new Exception("Route duplicated: " + entry.Value);
                }
            }

        }

    }
}