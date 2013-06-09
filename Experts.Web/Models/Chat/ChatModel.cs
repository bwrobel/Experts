using System.Collections.Generic;

namespace Experts.Web.Models.Chat
{
    public class ChatModel
    {
        public IEnumerable<ChatMessageViewModel> Messages { get; set; }

        public bool IsFrameOpen { get; set; }

        public bool IsModerator { get; set; }

        public bool IsOwner { get; set; }

        public bool IsViewOnly { get; set; }

        public bool AskForEmail { get; set; }
    }
}