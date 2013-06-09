using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Experts.Web.Filters;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Extensibility;

namespace Experts.Web.Utils.SiteMap
{
    public class PublicPagesDynamicNodeProvider : DynamicNodeProviderBase
    {
        private static readonly List<Tuple<string, string>> BlackList = new List<Tuple<string, string>>
                                                                            {
                                                                                new Tuple<string, string>(MVC.StaticPages.Name, MVC.StaticPages.ActionNames.Home),
                                                                                new Tuple<string, string>(MVC.StaticPages.Name, MVC.Error.ActionNames.Error404),
                                                                                new Tuple<string, string>(MVC.Thread.Name, MVC.Thread.ActionNames.Options),
                                                                                new Tuple<string, string>(MVC.Thread.Name, MVC.Thread.ActionNames.CategoryAttributes),
                                                                                new Tuple<string, string>(MVC.Catalog.Name, MVC.Catalog.ActionNames.SiteMapXml),
                                                                                new Tuple<string, string>(MVC.Launch.Name, MVC.Launch.ActionNames.Welcome)
                                                                            };

        private static readonly List<Tuple<string, string>> FrequentlyChanging = new List<Tuple<string, string>>
                                                                            {
                                                                                new Tuple<string, string>(MVC.Catalog.Name, MVC.Catalog.ActionNames.KeywordListExperts),
                                                                                new Tuple<string, string>(MVC.Catalog.Name, MVC.Catalog.ActionNames.KeywordListPhrases),
                                                                                new Tuple<string, string>(MVC.Catalog.Name, MVC.Catalog.ActionNames.KeywordListQuestions),
                                                                                new Tuple<string, string>(MVC.Thread.Name, MVC.Thread.ActionNames.CatalogQuestionList)
                                                                            };

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection()
        {
            var result = new List<DynamicNode>();

            var assembly = Assembly.GetExecutingAssembly();
            var controllers = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Controller)) && !t.Name.StartsWith("T4MVC_"));

            foreach (var controller in controllers)
            {
                var controllerName = controller.Name.Replace("Controller", string.Empty);
                var actions = controller.GetMethods()
                       .Where(m => m.ReturnType.IsSubclassOf(typeof(ActionResult)) || m.ReturnType == typeof(ActionResult))
                       .Where(m => !m.GetCustomAttributes(typeof(NonActionAttribute), true).Any())
                       .Where(m => m.IsPublic);

                foreach (var action in actions)
                {
                    var isOnBlackList = BlackList.Any(bl => bl.Item1 == controllerName && bl.Item2 == action.Name);
                    var isPartialAction = action.GetCustomAttributes(typeof (DefaultRoutingAttribute), true).Any();
                    var isPrivate = action.GetCustomAttributes(typeof(AuthorizeAttribute), true).Any() || action.GetCustomAttributes(typeof(AuthorizeRolesAttribute), true).Any();
                    var isPost = action.GetCustomAttributes(typeof (HttpPostAttribute), true).Any();
                    var hasParameters = action.GetParameters().Any(p => !p.IsOptional);

                    if (isOnBlackList || isPartialAction || isPrivate || isPost || hasParameters)
                        continue;

                    var node = new DynamicNode {Controller = controllerName, Action = action.Name};


                    var isChangingFrequently = FrequentlyChanging.Any(fc => fc.Item1 == controllerName && fc.Item2 == action.Name);

                    if (isChangingFrequently)
                        node.ChangeFrequency = ChangeFrequency.Daily;
                    else
                        node.ChangeFrequency = ChangeFrequency.Monthly;

                    result.Add(node);
                }
            }

            return result;
        }
    }
}