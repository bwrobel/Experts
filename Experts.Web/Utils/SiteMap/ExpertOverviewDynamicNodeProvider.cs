using System.Collections.Generic;
using Experts.Core.Utils;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Extensibility;
using Experts.Core.Repositories;

namespace Experts.Web.Utils.SiteMap
{
    public class ExpertOverviewDynamicNodeProvider : CustomDynamicNodeProviderBase
    {
        public ExpertOverviewDynamicNodeProvider()
            : base(ChangeFrequency.Daily)
        {
        }

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection()
        {
            var result = new List<DynamicNode>();

            var experts = RepositoryHelper.Repository.Expert.Find(query: e => e.ByPublic());
            foreach (var expert in experts)
            {
                var node = CreateNode(MVC.Profile.Name, MVC.Profile.ActionNames.ExpertOverview);
                node.RouteValues.Add("expertId", expert.Id);
                result.Add(node);
            }

            return result;
        }
    }
}