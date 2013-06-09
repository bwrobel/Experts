using Experts.Core.Entities;

namespace Experts.Web.Models.Threads
{
    public class SeoDetails
    {
        public int Id { get; set; }
        public string Phrase { get; set; }
        public Category Category { get; set; }
        public SEOKeywordType Type { get; set; }
        public SEOKeywordStatus Status { get; set; }
    }
}