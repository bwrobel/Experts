namespace Experts.Web.Models.Chat
{
    public class ChatMessageViewModel
    {
        public string AuthorName { get; set; }

        public string CreationDate { get; set; }

        public long Timestamp { get; set; }

        public string Text { get; set; }

        public bool IsRead { get; set; }

        public string Context { get; set; }
    }
}