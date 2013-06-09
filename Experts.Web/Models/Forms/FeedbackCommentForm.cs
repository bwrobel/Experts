using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Experts.Web.Models.Forms
{
    public class FeedbackCommentForm
    {
        [HiddenInput(DisplayValue = false)]
        public int FeedbackId { get; set; }

        [Display(Name = Resources.ThreadConstants.ThreadFeedbackComment, ResourceType = typeof(Resources.Thread))]
        [Required(ErrorMessageResourceName = Resources.ThreadConstants.ThreadFeedbackCommentRequired, ErrorMessageResourceType = typeof(Resources.Thread))]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
    }
}