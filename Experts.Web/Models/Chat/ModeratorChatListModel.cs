using Experts.Web.Models.Shared;

namespace Experts.Web.Models.Chat
{
    public class ModeratorChatListModel
    {
        public SortableGridModel<Core.Entities.Chat> GridModel { get; set; }
    }
}