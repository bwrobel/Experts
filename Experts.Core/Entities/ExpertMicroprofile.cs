namespace Experts.Core.Entities
{
    public class ExpertMicroprofile : IEntity
    {
        public int Id { get; set; }

        public string Position { get; set; }
        public string Description { get; set; }
    }
}
