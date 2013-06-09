using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Experts.Core.Entities;

namespace Experts.Web.Helpers
{
    public static class CategoryHelper
    {
        public static IEnumerable<SelectListItem> GetCategoryListItems(IEnumerable<Category> categories, string defaultItem)
        {
            var result = categories.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToList();
            result.Insert(0, new SelectListItem { Selected = true, Text = defaultItem, Value = string.Empty });
            return result;
        }
    }
}