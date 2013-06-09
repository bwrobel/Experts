using System.Collections.Generic;
using System.Data;
using Experts.Core.Data;
using Experts.Core.Entities;
using System.Linq;

namespace Experts.Core.Repositories
{
    public class CategoryRepository : EntityRepository<Category>
    {
        public CategoryRepository(DataContext db)
            :base(db)
        {}

        protected override IQueryable<Category> OrderedRows
        {
            get
            {
                return base.OrderedRows.OrderBy(c => c.Order);
            }
        }

        public decimal GetValue(int categoryId, ThreadPriority priority, ThreadVerbosity verbosity)
        {
            var price = Db.Prices.Single(p => p.Category.Id == categoryId && p.IntPriority == (int)priority && p.IntVerbosity == (int)verbosity);
            return price.Value;
        }

        public Category GetCategory(string name)
        {
            return Db.Categories.Single(c => c.Name == name);
        }

        public CategoryAttribute GetCategoryAttribute(int attributeId)
        {
            return Db.CategoryAttributes.Find(attributeId);
        }

        public void UpdateCategoryAttribute(CategoryAttribute attribute)
        {
            Db.Entry(attribute).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public void DeleteCategoryAttribute(CategoryAttribute attribute)
        {
            var threads = Db.Threads.Where(t => t.CategoryAttributes.Any(cav => cav.Attribute.Id == attribute.Id));
            foreach (var thread in threads)
            {
                var attributeValues = thread.CategoryAttributes.Where(cav => cav.Attribute.Id == attribute.Id).ToList();
                foreach (var attributeValue in attributeValues)
                {
                    thread.CategoryAttributes.Remove(attributeValue);
                    Db.CategoryAttributeValues.Remove(attributeValue);
                }
            }

            var experts = Db.Experts.Where(e => e.CategoryAttributes.Any(ecav => ecav.CategoryAttributes.Any(cav => cav.Attribute.Id == attribute.Id)));
            foreach (var expert in experts)
            {
                var expertAttributeValues = expert.CategoryAttributes.Where(ecav => ecav.CategoryAttributes.Any(cav => cav.Attribute.Id == attribute.Id));
                foreach (var expertAttributeValue in expertAttributeValues)
                {
                    var attributeValues = expertAttributeValue.CategoryAttributes.Where(cav => cav.Attribute.Id == attribute.Id).ToList();
                    foreach (var attributeValue in attributeValues)
                    {
                        expertAttributeValue.CategoryAttributes.Remove(attributeValue);
                        Db.CategoryAttributeValues.Remove(attributeValue);
                    }
                }
                Db.Entry(expert).State = EntityState.Modified;
            }
            Db.SaveChanges();

            foreach (var optionId in attribute.Options.Select(o => o.Id).ToList())
                DeleteCategoryAttributeOption(optionId);

            var parentAttributes = Db.CategoryAttributes.Where(ca => ca.ChildAttributes.Any(child => child.Id == attribute.Id));
            foreach (var parentAttribute in parentAttributes)
                parentAttribute.ChildAttributes.Remove(attribute);

            attribute.ChildAttributes.Clear();
            Db.CategoryAttributes.Remove(attribute);
            Db.SaveChanges();
        }

        public void DeleteCategoryAttributeOption(int optionId)
        {
            var option = Db.CategoryAttributeOptions.Find(optionId);
            Db.CategoryAttributeOptions.Remove(option);
            Db.SaveChanges();
        }
    }

    public static class CategoryQueryExtensions
    {
        public static IQueryable<Category> ByIds(this IQueryable<Category> query, IEnumerable<int> ids)
        {
            return query.Where(c => ids.Contains(c.Id));
        }
    }
}
