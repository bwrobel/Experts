using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Experts.Core.Entities;
using Experts.Web.Helpers;
using Experts.Web.Models.Shared;

namespace Experts.Web.Models.Threads
{
    public class QuestionListModel<T>
        where T : class
    {
        public QuestionListModel(IEnumerable<Category> availableCategories)
        {
            AvailableCategories = CategoryHelper.GetCategoryListItems(availableCategories,
                                                                      Resources.Thread.AllCategories);
        }

        public IEnumerable<SelectListItem> AvailableCategories { get; private set; }

        public SortableGridModel<T> GridModel { get; set; }

        public Category SelectedCategory { get; set; }
    }
}