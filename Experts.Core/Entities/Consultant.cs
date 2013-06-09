namespace Experts.Core.Entities
{
    public class Consultant : IEntity
    {
        public int Id { get; set; }

        public virtual User User { get; set; }

        public string PublicName { get; set; }
    }
}
