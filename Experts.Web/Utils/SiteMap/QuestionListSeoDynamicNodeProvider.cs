using MvcSiteMapProvider;

namespace Experts.Web.Utils.SiteMap
{
    public class QuestionListSeoDynamicNodeProvider : SeoDynamicNodeProvider
    {
        public QuestionListSeoDynamicNodeProvider()
            : base(MVC.Catalog.ActionNames.QuestionListSeo, ChangeFrequency.Weekly)
        {
        }
    }
}