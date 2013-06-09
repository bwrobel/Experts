using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Experts.Core.Entities
{
    public class ExpertCategoryAttributeValues : IEntity
    {
        public ExpertCategoryAttributeValues()
        {
            CategoryAttributes = new Collection<CategoryAttributeValue>();
        }

        public string ExpertReason { get; set; }
        public int Id { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<CategoryAttributeValue> CategoryAttributes { get; private set; }
    }
}
