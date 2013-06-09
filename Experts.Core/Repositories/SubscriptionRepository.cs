using Experts.Core.Data;
using Experts.Core.Entities;

namespace Experts.Core.Repositories
{
    public class SubscriptionRepository : AuditableRepository<Subscription>
    {
        public SubscriptionRepository(DataContext db)
            : base(db)
        {
        }
    }
}
