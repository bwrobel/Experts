using System.Collections.Generic;
using Experts.Core.Entities;
using Experts.Web.Models.Forms;

namespace Experts.Web.Models.Account
{
    public class ExpertProfileFormModel
    {
        public IEnumerable<Category> AvailableCategories { get; set; }

        public ExpertProfileForm ExpertProfileForm { get; set; }

        public ExpertProfileFormModel(IEnumerable<Category> availableCategories)
        {
            AvailableCategories = availableCategories;
        }

        public ExpertProfileFormModel()
        {
        }
    }
}