using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Experts.Core.Entities
{
    public class CategoryAttribute : IEntity
    {
        public CategoryAttribute()
        {
            Options = new Collection<CategoryAttributeOption>();
            ChildAttributes = new Collection<CategoryAttribute>();
            ParentOptions = new Collection<CategoryAttributeOption>();
        }

        public int Id { get; set; }

        public int IntType { get; set; }
        public CategoryAttributeType Type
        {
            get { return (CategoryAttributeType)IntType; }
            set { IntType = (int)value; }
        }

        public string Name { get; set; }

        public int Importance { get; set; }

        public virtual ICollection<CategoryAttributeOption> Options { get; private set; }

        public virtual ICollection<CategoryAttribute> ChildAttributes { get; private set; }
        public virtual ICollection<CategoryAttributeOption> ParentOptions { get; set; }
    }

    public enum CategoryAttributeType
    {
        SingleLineText = 0,
        MultiLineText = 1,
        SingleSelect = 2,
        MultiSelect = 3
    }

    public class Importance
    {
        public const int Highest = 13;
        public const int High = 8;
        public const int Medium = 5;
        public const int LowerMedium = 3;
        public const int Low = 2;
        public const int Lowest = 1;
        public const int None = 0;
    }
}
