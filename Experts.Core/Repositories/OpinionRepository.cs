using System.Linq;
using Experts.Core.Data;
using Experts.Core.Entities;

namespace Experts.Core.Repositories
{
    public class OpinionRepository : EntityRepository<Opinion>
    {
        public OpinionRepository(DataContext db)
            : base(db)
        { }
        
        public override void Add(Opinion opinion)
        {
            base.Add(opinion);
        }
    }

    public static class OpinionQueryExtensions
    {
        public static IQueryable<Opinion> ByAuthorType(this IQueryable<Opinion> query, AuthorType authorType)
        {
            return query.Where(uo => uo.AuthorTypeInt == (int) authorType);
        }

        public static IQueryable<Opinion> ByCategoryId(this IQueryable<Opinion> query, int categoryId)
        {
            return query.Where(uo => uo.Categories.Any(c => c.Id == categoryId));
        }

        public static IQueryable<Opinion> ByGeneral(this IQueryable<Opinion> query)
        {
            return query.Where(uo => uo.IsGeneral);
        }
    }
}
