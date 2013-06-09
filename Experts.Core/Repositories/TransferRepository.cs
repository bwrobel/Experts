using System.Linq;
using Experts.Core.Data;
using Experts.Core.Entities;

namespace Experts.Core.Repositories
{
    public class TransferRepository : EntityRepository<Transfer>
    {
        public TransferRepository(DataContext db)
            : base(db)
        { }

    }

    public static class TransferQueryExtensions
    {
        public static IQueryable<Transfer> ByOwner(this IQueryable<Transfer> query, User owner)
        {
            return query.Where(t => t.Owner.Id == owner.Id);
        }
        public static IQueryable<Transfer> ByPendingStatus(this IQueryable<Transfer> query, bool isPending)
        {
            return query.Where(t => t.IsPending == isPending);
        }
    }
}
