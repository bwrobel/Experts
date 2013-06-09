using System.Linq;
using Experts.Core.Data;
using Experts.Core.Entities;

namespace Experts.Core.Repositories
{
    public class FeedbackRepository : AuditableRepository<Feedback>
    {
        public FeedbackRepository(DataContext db)
            : base(db)
        {
        }
    }

    public static class FeedbackQueryExtensions
    {
        public static IQueryable<Feedback> ByCategory(this IQueryable<Feedback> query, int? categoryId)
        {
            if (!categoryId.HasValue)
                return query;

            return query.Where(f => f.Thread.Category.Id == categoryId);
        }
    }
}
