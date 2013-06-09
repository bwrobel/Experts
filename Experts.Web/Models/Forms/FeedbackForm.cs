using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Experts.Core.Entities;

namespace Experts.Web.Models.Forms
{
    public class FeedbackForm
    {
        [Required]
        [HiddenInput(DisplayValue = false)]
        public int ThreadId { get; set; }

        [Display(Name = Resources.ThreadConstants.FeedbackMark, ResourceType = typeof(Resources.Thread))]
        public FeedbackMark Mark { get; set; }

        [Display(Name = Resources.ThreadConstants.FeedbackContent, ResourceType = typeof(Resources.Thread))]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceName = Resources.ThreadConstants.ThreadFeedbackRequired, ErrorMessageResourceType = typeof(Resources.Thread))]
        [StringLength(250, MinimumLength = 2, ErrorMessageResourceName = Resources.ThreadConstants.ThreadFeedbackTooShort, ErrorMessageResourceType = typeof(Resources.Thread))]
        public string Content { get; set; }
    }
}