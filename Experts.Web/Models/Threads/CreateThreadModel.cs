using System.Collections.Generic;
using Experts.Core.Entities;

namespace Experts.Web.Models.Threads
{
    public class CreateThreadModel :ThreadFormModel
    {
        public CreateThreadModel(IEnumerable<Category> availableCategories)
            : base(availableCategories)
        {
        }

        public CreateThreadModel()
        {
        }
    }

    
}