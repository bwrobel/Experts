using System.Collections.Generic;
using Experts.Core.Entities;
using Experts.Web.Models.Forms;

namespace Experts.Web.Models.Account
{
    public class ExpertSignUpModel : ExpertProfileFormModel
    {
        public ProfileForm UserProfileForm { get; set; }

        public ExpertSignUpModel(IEnumerable<Category> availableCategories)
            : base(availableCategories)
        {
        }

        public ExpertSignUpModel()
        {
        }
    }
}