using Resources;

namespace Experts.Core.Entities.Events
{
    public abstract class StatisticsEvent : Event
    {
        public override string IconPostfix { get { return Icon.Tasks; } }
    }

    public class ActiveUsersStatisticsEvent : StatisticsEvent
    {
        public override string Name { get { return Administration.ActiveUsersStatisticsEventName; } }
        public override string Message { get { return string.Format(Administration.ActiveUsersStatisticsEventMessage, Data); } }
    }

    public class ActiveExpertStatisticsEvent : StatisticsEvent
    {
        public override string Name { get { return Administration.ActiveExpertsStatisticsEventName; } }
        public override string Message { get { return string.Format(Administration.ActiveExpertsStatisticsEventMessage, Data); } }
    }

    public class NewSeoKeywordsStatisticsEvent : StatisticsEvent
    {
        public override string IconPostfix { get { return Icon.Ok; } }
        public override string Name { get { return Administration.NewSeoKeywordsStatisticsEventName; } }
        public override string Message { get { return string.Format(Administration.NewSeoKeywordsStatisticsEventMessage, Data); } }
    }

    public class LastDayThreadsStatisticsEvent : StatisticsEvent
    {
        public override string Name { get { return Administration.LastDayThreadsStatisticsEventName; } }
        public override string Message
        {
            get
            {
                var date = OccurenceDate.AddDays(-1).ToShortDateString();
                return string.Format(Administration.LastDayThreadsStatisticsEventMessage, date, Data);
            }
        }
    }
}
