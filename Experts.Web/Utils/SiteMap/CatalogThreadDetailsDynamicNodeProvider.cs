using System.Collections.Generic;
using Experts.Core.Entities;
using Experts.Core.Utils;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Extensibility;
using Experts.Core.Repositories;
using Experts.Web.Helpers;

namespace Experts.Web.Utils.SiteMap
{
    public class CatalogThreadDetailsDynamicNodeProvider : CustomDynamicNodeProviderBase
    {
        public CatalogThreadDetailsDynamicNodeProvider()
            : base(ChangeFrequency.Monthly)
        {
        }

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection()
        {
            var result = new List<DynamicNode>();

            var threads =
                RepositoryHelper.Repository.Thread.Find(
                    query: t => t.BySanitizationStatus(ThreadSanitizationStatus.Sanitized));
            foreach (var thread in threads)
            {
                var node = CreateNode(MVC.Thread.Name, MVC.Thread.ActionNames.CatalogThreadDetails);
                node.RouteValues.Add("threadId", thread.Id);
                node.RouteValues.Add("title", thread.Name.ToUrlPart());

                result.Add(node);
            }

            return result;
        }
    }
}