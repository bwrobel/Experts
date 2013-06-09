using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Experts.Web.Models.Forms
{
    public class AdditionalServiceForm
    {
        [HiddenInput(DisplayValue = false)]
        public int ThreadId { get; set; }

        [Display(Name = Resources.ThreadConstants.ProposeAdditionalServiceTitle, ResourceType = typeof(Resources.Thread))]
        [Required(ErrorMessageResourceName = Resources.ThreadConstants.ProposeAdditionalServiceTitleRequired, ErrorMessageResourceType = typeof(Resources.Thread))]
        public string Title { get; set; }

        [Display(Name = Resources.ThreadConstants.ProposeAdditionalServiceComment, ResourceType = typeof(Resources.Thread))]
        [Required(ErrorMessageResourceName = Resources.ThreadConstants.ProposeAdditionalServiceCommentRequired, ErrorMessageResourceType = typeof(Resources.Thread))]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        [Display(Name = Resources.ThreadConstants.ProposeAdditionalServiceValue, ResourceType = typeof(Resources.Thread))]
        [Required(ErrorMessageResourceName = Resources.ThreadConstants.ProposeAdditionalServiceValueRequired, ErrorMessageResourceType = typeof(Resources.Thread))]
        public decimal? Value { get; set; }
    }
}