using Resources;

namespace Experts.Core.Entities.Events
{
    public abstract class ThreadEvent : Event
    {
        public virtual Thread RelatedThread { get; set; }
    }

    public class ThreadIssueReportEvent : ThreadEvent
    {
        public override string Name { get { return Administration.ThreadIssueReportEventName; } }
        public override string IconPostfix { get { return Icon.ExclamationSign; } }
        public override string Message { get { return string.Format(Administration.ThreadIssueReportEventMessage, RelatedThread.Id, Data); } }
    }

    public class NewThreadForExpertEvent : ThreadEvent
    {
        public override string Name { get { return Administration.NewThreadForExpertEventName; } }
        public override string IconPostfix { get { return Icon.PlusSign; } }
        public override string Message { get { return string.Format(Administration.NewThreadForExpertEventMessage, Data); } }
    }

    public class ThreadOccupiedEvent : ThreadEvent
    {
        public override string Name { get { return Administration.ThreadOccupiedEventName; } }
        public override string IconPostfix { get { return Icon.Stop; } }
        public override string Message { get { return string.Format(Administration.ThreadOccupiedEventMessage, RelatedThread.Name, Data); } }
    }

    public class ThreadReleasedEvent : ThreadEvent
    {
        public override string Name { get { return Administration.ThreadReleasedEventName; } }
        public override string IconPostfix { get { return Icon.Play; } }
        public override string Message { get { return string.Format(Administration.ThreadReleasedEventMessage, RelatedThread.Name, Data); } }
    }

    public class NoAnswerEvent : ThreadEvent
    {
        public override string Name { get { return Administration.NoAnswerEventName; } }
        public override string IconPostfix { get { return Icon.Stop; } }
        public override string Message { get { return string.Format(Administration.NoAnswerEventMessage, RelatedThread.Name); } }
    }

    public class DetailsRequestEvent : ThreadEvent
    {
        public override string Name { get { return Administration.DetailsRequestEventName; } }
        public override string IconPostfix { get { return Icon.User; } }
        public override string Message { get { return string.Format(Administration.DetailsRequestEventMessage, Data, RelatedThread.Id); } }
    }

    public class QuestionAnsweredEvent : ThreadEvent
    {
        public override string Name { get { return Administration.QuestionAnsweredEventName; } }
        public override string IconPostfix { get { return Icon.User; } }
        public override string Message { get { return string.Format(Administration.QuestionAnsweredEventMessage, Data, RelatedThread.Id); } }
    }

    public class GiveUpEvent : ThreadEvent
    {
        public override string Name { get { return Administration.GiveUpEventName; } }
        public override string IconPostfix { get { return Icon.User; } }
        public override string Message { get { return string.Format(Administration.GiveUpEventMessage, Data, RelatedThread.Id); } }
    }

    public class AuthorAddedPostEvent : ThreadEvent
    {
        public override string Name { get { return Administration.AuthorAddedPostName; } }
        public override string IconPostfix { get { return Icon.User; } }
        public override string Message { get { return string.Format(Administration.AuthorAddedPostMessage, RelatedThread.Id); } }
    }

    public class AuthorAcceptedAnswerEvent : ThreadEvent
    {
        public override string Name { get { return Administration.AuthorAcceptedAnswerName; } }
        public override string IconPostfix { get { return Icon.User; } }
        public override string Message { get { return string.Format(Administration.AuthorAcceptedAnswerMessage,RelatedThread.Id, Data); } }
    }

    public class AuthorCreatedFeedbackEvent : ThreadEvent
    {
        public override string Name { get { return Administration.AuthorCreatedFeedbackName; } }
        public override string IconPostfix { get { return Icon.User; } }
        public override string Message { get { return string.Format(Administration.AuthorCreatedFeedbackMessage, RelatedThread.Id, Data); } }
    }

    public class ExpertCommentedFeedbackEvent : ThreadEvent
    {
        public override string Name { get { return Administration.ExpertCommentedFeedbackEventName; } }
        public override string IconPostfix { get { return Icon.User; } }
        public override string Message { get { return string.Format(Administration.ExpertCommentedFeedbackEventMessage, Data, RelatedThread.Id); } }
    }

    public class AwaitingExpertResponseEvent : ThreadEvent
    {
        public override string Name { get { return Administration.AwaitingExpertResponseName; } }
        public override string IconPostfix { get { return Icon.User; } }
        public override string Message { get { return string.Format(Administration.AwaitingExpertResponseMessage, RelatedThread.Id); } }
    }

    public class PriceProposalAcceptedEvent : ThreadEvent
    {
        public override string Name { get { return Administration.PriceProposalAcceptedEventName; } }
        public override string IconPostfix { get { return Icon.User; } }
        public override string Message { get { return string.Format(Administration.PriceProposalAcceptedEventMessage, RelatedThread.Id, Data); } }
    }

    public class AcceptAnswerReminderEvent : ThreadEvent
    {
        public override string Name { get { return Administration.AcceptAnswerReminderEventName; } }
        public override string IconPostfix { get { return Icon.User; } }
        public override string Message { get { return string.Format(Administration.AcceptAnswerReminderEventMessage, RelatedThread.Id); } }
    }

    public class NewAdditionalServiceEvent : ThreadEvent
    {
        public override string Name { get { return Administration.NewAdditionalServiceEventName; } }
        public override string IconPostfix { get { return Icon.User; } }
        public override string Message { get { return string.Format(Administration.NewAdditionalServiceEventMessage, Data, RelatedThread.Id); } }
    }

    public class AdditionalServicePayedEvent : ThreadEvent
    {
        public override string Name { get { return Administration.AdditionalServicePayedName; } }
        public override string IconPostfix { get { return Icon.User; } }
        public override string Message { get { return string.Format(Administration.AdditionalServicePayedMessage, Data, RelatedThread.Id); } }
    }

    public class ThreadClosedEvent : ThreadEvent
    {
        public override string Name { get { return Administration.ThreadClosedEventName; } }
        public override string IconPostfix { get { return Icon.User; } }
        public override string Message { get { return string.Format(Administration.ThreadClosedEventMessage, RelatedThread.Id); } }
    }

    public class NewThreadEvent : ThreadEvent
    {
        public override string Name { get { return Administration.NewThreadEventName; } }
        public override string IconPostfix { get { return Icon.User; } }
        public override string Message { get { return string.Format(Administration.NewThreadEventMessage, RelatedThread.Id, Data); } }
    }

    public class ThreadPayedEvent : ThreadEvent
    {
        public override string Name { get { return Administration.ThreadPayedEventName; } }
        public override string IconPostfix { get { return Icon.User; } }
        public override string Message { get { return string.Format(Administration.ThreadPayedEventMessage, RelatedThread.Id); } }
    }

    public class ExpertThreadNotifiedEvent : ThreadEvent
    {
        public override string Name { get { return Administration.ExpertThreadNotifiedEventName; } }
        public override string IconPostfix { get { return Icon.User; } }
        public override string Message { get { return string.Format(Administration.ExpertThreadNotifiedEventMessage, Data, RelatedThread.Id); } }
    }

    public class UserDefinedPriceEvent : ThreadEvent
    {
        public override string Name { get { return Administration.UserDefinedPriceEventName; } }
        public override string IconPostfix { get { return Icon.Tag; } }
        public override string Message { get { return string.Format(Administration.UserDefinedPriceEventMessage, RelatedThread.Id); } }
    }

    public class ExpertPriceProposalEvent : ThreadEvent
    {
        public override string Name { get { return Administration.ExpertPriceProposalName; } }
        public override string IconPostfix { get { return Icon.Tag; } }
        public override string Message { get { return string.Format(Administration.ExpertPriceProposalMessage, Data, RelatedThread.Id); } }
    }
}