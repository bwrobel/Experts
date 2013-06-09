using MvcSiteMapProvider;

namespace Experts.Web.Utils.SiteMap
{
    public class PhraseDetailsDynamicNodeProvider : SeoDynamicNodeProvider
    {
        public PhraseDetailsDynamicNodeProvider()
            : base(MVC.Catalog.ActionNames.PhraseDetails, ChangeFrequency.Weekly)
        {
        }
    }
}