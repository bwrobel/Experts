using System.Collections.Generic;
using Experts.Core.Entities;
using Experts.Core.Entities.Events;
using Experts.Core.ViewModels;

namespace Experts.Web.Models.Threads
{
    public class ExpertOverviewQuestion : ThreadFormModel
    {
        public ExpertOverviewViewModel ExpertOverviewViewModel { get; set; }

        public IEnumerable<Event> ExpertEvents { get; set; }

        public ExpertOverviewQuestion(IEnumerable<Category> availableCategories)
            : base(availableCategories)
        {
        }

        public ExpertOverviewQuestion(){}
    }
}