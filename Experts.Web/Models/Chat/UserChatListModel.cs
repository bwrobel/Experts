using Experts.Web.Models.Shared;

namespace Experts.Web.Models.Chat
{
    public class UserChatListModel
    {
        public int? CurrentChatId { get; set; }

        public SortableGridModel<Core.Entities.Chat> UserChats { get; set; }
    }
}