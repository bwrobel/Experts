namespace Experts.Web.Models.Chat
{
    public class ChatsOverviewModel
    {
        public ChatsOverviewFilter Filter { get; set; }
        public int Page { get; set; }
    }

    public enum ChatsOverviewFilter
    {
        Open,
        Closed,
        WaitingForResponse,
    }
}