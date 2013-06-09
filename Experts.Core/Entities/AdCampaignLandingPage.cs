using System.ComponentModel.DataAnnotations;

namespace Experts.Core.Entities
{
    public class AdCampaignLandingPage
    {
        [Key]
        public string Code { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public virtual Category DefaultCategory { get; set; }
    }
}
