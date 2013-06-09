using MvcSiteMapProvider;
using MvcSiteMapProvider.Extensibility;

namespace Experts.Web.Utils.SiteMap
{
    public abstract class CustomDynamicNodeProviderBase : DynamicNodeProviderBase
    {
        private readonly ChangeFrequency _nodeChangeFrequency;

        protected CustomDynamicNodeProviderBase(ChangeFrequency nodeChangeFrequency)
        {
            _nodeChangeFrequency = nodeChangeFrequency;
        }

        protected DynamicNode CreateNode(string controller, string action)
        {
            return new DynamicNode
                       {
                           Controller = controller,
                           Action = action,
                           ChangeFrequency = _nodeChangeFrequency
                       };
        }
    }
}