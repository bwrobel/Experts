using System.Collections.Generic;
using System.Linq;
using Experts.Core.Data;
using Experts.Core.Entities;

namespace Experts.Core.Repositories
{
    public class SEOKeywordRepository:EntityRepository<SEOKeyword>
    {
        public SEOKeywordRepository(DataContext db) : base(db)
        {
        }

        public SEOKeyword GetToModerate()
        {
            return Db.SEOKeyword.OrderByDescending(sk => new { sk.IntSource, sk.HitCount, sk.Id })
                                .FirstOrDefault(sk => sk.IntStatus == (int)SEOKeywordStatus.New);

        }
    }

    public static class SEOKeywordQueryExtensions
    {
        public static IQueryable<SEOKeyword> ByPhrase(this IQueryable<SEOKeyword> query, string keywordPhrase)
        {
            return query.Where(kp => kp.Phrase == keywordPhrase); 
        }

        public static IQueryable<SEOKeyword> ByCategories(this IQueryable<SEOKeyword> query, IEnumerable<Category> categories)
        {
            var categoryIds = categories.Select(c => c.Id).ToList();
            return query.Where(k => categoryIds.Contains(k.Category.Id));
        }

        public static IQueryable<SEOKeyword> ByStatus(this IQueryable<SEOKeyword> query, SEOKeywordStatus status)
        {
            return query.Where(k => k.IntStatus == (int)status);
        }

        public static IQueryable<SEOKeyword> ByType(this IQueryable<SEOKeyword> query, SEOKeywordType type)
        {
            return query.Where(k => k.IntType == (int)type);
        }
    }
}
