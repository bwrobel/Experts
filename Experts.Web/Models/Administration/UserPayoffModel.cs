using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Experts.Web.Models.Administration
{
    public class UserPayoffModel
    {
        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }

        [Display(Name = Resources.AdministrationConstants.UserPayoffAmount, ResourceType = typeof(Resources.Administration))]
        [Required(ErrorMessageResourceName = Resources.AdministrationConstants.UserPayoffAmountRequired, ErrorMessageResourceType = typeof(Resources.Administration))]
        public decimal? Amount { get; set; }

        [Display(Name = Resources.AdministrationConstants.UserPayoffComment, ResourceType = typeof(Resources.Administration))]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
    }
}