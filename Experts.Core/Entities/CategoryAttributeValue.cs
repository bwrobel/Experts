using System.Collections.Generic;

namespace Experts.Core.Entities
{
    public class CategoryAttributeValue : IEntity
    {
        public int Id { get; set; }
        public virtual CategoryAttribute Attribute { get; set; }
        public virtual ICollection<CategoryAttributeOption> SelectedOptions { get; set; }
        public virtual string Value { get; set; }
    }
}