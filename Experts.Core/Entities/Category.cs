using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Experts.Core.Entities
{
    public class Category : IEntity
    {
        public Category()
        {
            Attributes = new Collection<CategoryAttribute>();
            Prices = new Collection<Price>();
            Experts = new Collection<Expert>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LongDescription { get; set; }
        public string Photo { get; set; }
        //public bool IsMain { get; set; }
        public int Order { get; set; }

        public virtual ICollection<Price> Prices { get; private set; }
        public virtual ICollection<Expert> Experts { get; private set; }
        public virtual ICollection<CategoryAttribute> Attributes { get; private set; }
        public virtual ICollection<Opinion> Opinions { get; private set; }
    }
}
