using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Experts.Web.Models.Forms
{
    public class ApplicationErrorForm
    {
        [HiddenInput(DisplayValue = false)]
        public int EventId { get; set; }

        [Display(Name = Resources.GlobalConstants.ErrorFormDescriptionLabel, ResourceType = typeof(Resources.Global))]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = Resources.GlobalConstants.ErrorFormEmailLabel, ResourceType = typeof(Resources.Global))]
        public string Email { get; set; }
    }
}