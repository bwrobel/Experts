using Experts.Core.Entities;
using Experts.Web.Models.Forms;
using Experts.Web.Models.Shared;

namespace Experts.Web.Models.Chat
{
    public class ChatDetailsModel
    {
        public bool ModeratorView { get; set; }

        public Core.Entities.Chat Chat { get; set; }

        public SortableGridModel<ChatMessage> GridModel { get; set; }

        public ChatMessageForm ChatMessageForm { get; set; }
    }   

}