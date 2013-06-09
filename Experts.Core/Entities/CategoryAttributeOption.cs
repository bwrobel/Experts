namespace Experts.Core.Entities
{
    public class CategoryAttributeOption : IEntity
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public decimal PriceModifier { get; set; }

        public virtual CategoryAttribute Attribute { get; set; }
    }
}