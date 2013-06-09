using System.Collections.Generic;
using Experts.Core.Entities;
using Experts.Core.Utils;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Extensibility;
using Experts.Core.Repositories;
using Experts.Web.Helpers;

namespace Experts.Web.Utils.SiteMap
{
    public class SeoDynamicNodeProvider : CustomDynamicNodeProviderBase
    {
        private readonly string _catalogActionName;

        public SeoDynamicNodeProvider(string catalogActionName, ChangeFrequency nodeChangeFrequency)
            : base(nodeChangeFrequency)
        {
            _catalogActionName = catalogActionName;
        }

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection()
        {
            var result = new List<DynamicNode>();

            var keywords = RepositoryHelper.Repository.SEOKeyword.Find(query: q => q.ByStatus(SEOKeywordStatus.Processed));
            foreach (var keyword in keywords)
            {
                var node = CreateNode(MVC.Catalog.Name, _catalogActionName);
                node.RouteValues.Add("keywordId", keyword.Id);
                node.RouteValues.Add("title", keyword.Phrase.ToUrlPart());

                result.Add(node);
            }

            return result;
        }
    }
}