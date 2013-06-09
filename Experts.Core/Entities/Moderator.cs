namespace Experts.Core.Entities
{
    public class Moderator : IEntity
    {
        public int Id { get; set; }

        public virtual User User { get; set; }

        public string PublicName { get; set; }
    }
}
