using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Experts.Web.Models.Forms
{
    public class IncreaseExpertValueForm
    {
        [HiddenInput(DisplayValue = false)]
        public int ThreadId { get; set; }

        [Display(Name = Resources.ThreadConstants.NewExpertValue, ResourceType = typeof(Resources.Thread))]
        [Required(ErrorMessageResourceName = Resources.ThreadConstants.NewExpertValueRequired, ErrorMessageResourceType = typeof(Resources.Thread))]
        public decimal? NewExpertValue { get; set; }
    }
}