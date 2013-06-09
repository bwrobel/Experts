using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Experts.Web.Models.Forms
{
    public class GiveUpForm
    {
        [HiddenInput(DisplayValue = false)]
        public int ThreadId { get; set; }

        [Display(Name = Resources.ThreadConstants.ThreadGiveUpComment, ResourceType = typeof(Resources.Thread))]
        [Required(ErrorMessageResourceName = Resources.ThreadConstants.ThreadGiveUpCommentRequired, ErrorMessageResourceType = typeof(Resources.Thread))]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
    }
}