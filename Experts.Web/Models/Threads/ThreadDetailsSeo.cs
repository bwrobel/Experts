using System.Collections.Generic;
using Experts.Core.Entities;

namespace Experts.Web.Models.Threads
{
    public class ThreadDetailsSeo : ThreadFormModel
    {
        public SeoDetails SeoDetails { get; set; }

        public ThreadDetailsSeo(IEnumerable<Category> availableCategories)
            : base(availableCategories)
        {
        }

        public ThreadDetailsSeo() { }

        public IEnumerable<Thread> Threads { get; set; }

        public IEnumerable<Expert> Experts { get; set; }
    }
}