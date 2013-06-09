using System.Collections.Generic;
using Experts.Core.Entities;

namespace Experts.Web.Models.Threads
{
    public class CatalogThreadQuestion : ExpertOverviewQuestion
    {
        public Thread Thread { get; set; }

        public CatalogThreadQuestion(IEnumerable<Category> availableCategories)
            : base(availableCategories)
        {
        }

        public CatalogThreadQuestion(){}
    }
}