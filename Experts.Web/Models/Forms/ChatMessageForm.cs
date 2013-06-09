using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Experts.Web.Validation;

namespace Experts.Web.Models.Forms
{
    public class ChatMessageForm
    {
        [AutoFocus]
        [Display(Name = Resources.AccountConstants.ChatMessageText, ResourceType = typeof(Resources.Account))]
        [Required(ErrorMessageResourceName = Resources.AccountConstants.ChatMessageTextRequired, ErrorMessageResourceType = typeof(Resources.Account))]
        public string Text { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? ChatId { get; set; }
    }
}