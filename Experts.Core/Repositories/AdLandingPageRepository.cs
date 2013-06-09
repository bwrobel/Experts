using Experts.Core.Data;
using Experts.Core.Entities;

namespace Experts.Core.Repositories
{
    public class AdCampaignLandingPageRepository : DbRepository
    {
        public AdCampaignLandingPageRepository(DataContext db) : base(db)
        {
        }

        public AdCampaignLandingPage Get(string code)
        {
            return Db.AdCampaignLandingPages.Find(code);
        }
    }
}
