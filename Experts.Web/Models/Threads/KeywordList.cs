using System.Collections.Generic;
using System.Web.Mvc;
using Experts.Core.Entities;
using Experts.Web.Helpers;
using Experts.Web.Models.Shared;

namespace Experts.Web.Models.Threads
{
    public class KeywordList
    {
        public KeywordList(IEnumerable<Category> availableCategories)
        {
            AvailableCategories = CategoryHelper.GetCategoryListItems(availableCategories, Resources.Thread.AllCategories);
        }

        public IEnumerable<SelectListItem> AvailableCategories { get; private set; }

        public SortableGridModel<SEOKeyword> GridModel { get; set; }

        public Category SelectedCategory { get; set; }
    }
}