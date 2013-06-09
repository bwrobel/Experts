using MvcSiteMapProvider;

namespace Experts.Web.Utils.SiteMap
{
    public class ExpertListSeoDynamicNodeProvider : SeoDynamicNodeProvider
    {
        public ExpertListSeoDynamicNodeProvider()
            : base(MVC.Catalog.ActionNames.ExpertListSeo, ChangeFrequency.Weekly)
        {
        }
    }
}