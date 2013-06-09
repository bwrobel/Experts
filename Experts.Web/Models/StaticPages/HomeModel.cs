using System.Collections.Generic;
using Experts.Core.Entities;
using Experts.Web.Models.Threads;

namespace Experts.Web.Models.StaticPages
{
    public class HomeModel : ThreadFormModel
    {
        public HomeModel(IEnumerable<Category> availableCategories)
            : base(availableCategories)
        {
        }

        public HomeModel() { }
    }

    public class AdCampaignHomeModel : HomeModel
    {
        public AdCampaignHomeModel(IEnumerable<Category> availableCategories, AdCampaignLandingPage landingPage)
            : base(availableCategories)
        {
            LandingPage = landingPage;
        }

        public AdCampaignHomeModel() { }
        public AdCampaignLandingPage LandingPage { get; set; }
    }
}