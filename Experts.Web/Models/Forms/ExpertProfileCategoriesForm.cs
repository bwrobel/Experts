using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Experts.Web.Models.Forms
{
    public class ExpertProfileCategoriesForm
    {
        [Display(Name = Resources.AccountConstants.SelectedCategories, ResourceType = typeof(Resources.Account))]
        public IEnumerable<int> SelectedCategories { get; set; }
    }
}