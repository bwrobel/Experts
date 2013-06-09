using System.Collections.Generic;
using Experts.Core.ViewModels;

namespace Experts.Web.Models.Threads
{
    public class MultiExpertBoxModel
    {
        public ExpertOverviewViewModel InitialExpert { get; set; }

        public IEnumerable<int> ExpertIds { get; set; }

        public bool AreFeedbacksVisible { get; set; }

        public string Title { get; set; }
    }
}