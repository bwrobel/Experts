using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Experts.Core.Entities;
using Experts.Web.Validation;

namespace Experts.Web.Models.Forms
{
    public class PostForm
    {
        [Required(ErrorMessageResourceName = Resources.ThreadConstants.PostContentRequired, ErrorMessageResourceType = typeof(Resources.Thread))]
        public string Content { get; set; }

        [Display(Name = Resources.ThreadConstants.PostType, ResourceType = typeof(Resources.Thread))]
        [Required(ErrorMessageResourceName = Resources.ThreadConstants.PostTypeNotSelected, ErrorMessageResourceType = typeof(Resources.Thread))]
        public PostType Type { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public int ThreadId { get; set; }
    }
}