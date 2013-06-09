using Resources;

namespace Experts.Core.Entities.Events
{
    public abstract class AccountEvent : Event
    {
        public virtual User RelatedUser { get; set; }
    }

    public class UserRegisteredEvent : AccountEvent
    {
        public override string Name { get { return Administration.UserRegisteredEventName; } }
        public override string IconPostfix { get { return Icon.User; } }
        public override string Message { get { return string.Format(Administration.UserRegisteredEventMessage, RelatedUser.Email, RelatedUser.Id); } }
    }

    public class UserAccountActivatedEvent : AccountEvent
    {
        public override string Name { get { return Administration.UserAccountActivatedEventName; } }
        public override string IconPostfix { get { return Icon.Check; } }
        public override string Message { get { return string.Format(Administration.UserAccountActivatedEventMessage, RelatedUser.Email, RelatedUser.Id); } }
    }

    public class ExpertRegisteredEvent : AccountEvent
    {
        public override string Name { get { return Administration.ExpertRegisteredEventName; } }
        public override string IconPostfix { get { return Icon.Briefcase; } }
        public override string Message { get { return string.Format(Administration.ExpertRegisteredEventMessage, RelatedUser.Expert.PublicName, RelatedUser.Email, RelatedUser.Id); } }
    }

    public class ExpertQualificationsChangedEvent : AccountEvent
    {
        public override string Name { get { return Administration.ExpertQualificationsChangedEventName; } }
        public override string IconPostfix { get { return Icon.Briefcase; } }
        public override string Message { get { return string.Format(Administration.ExpertQualificationsChangedEventMessage, RelatedUser.Expert.PublicName); } }
    }

    public class CashPaymentEvent : AccountEvent
    {
        public override string Name { get { return Administration.CashPaymentEventName; } }
        public override string IconPostfix { get { return Icon.Briefcase; } }
        public override string Message { get { return string.Format(Administration.CashPaymentMessage, RelatedUser.Expert.Id); } }
    }

    public class UserChangedEmailEvent : AccountEvent
    {
        public override string Name { get { return Administration.UserChangedEmailEventName; } }
        public override string IconPostfix { get { return Icon.Briefcase; } }
        public override string Message { get { return string.Format(Administration.UserChangedEmailEventMessage, RelatedUser.Id, Data); } }
    }

    public class UserChangedPasswordEvent : AccountEvent
    {
        public override string Name { get { return Administration.UserChangedPasswordEventName; } }
        public override string IconPostfix { get { return Icon.Briefcase; } }
        public override string Message { get { return string.Format(Administration.UserChangedPasswordEventMessage, RelatedUser.Id); } }
    }

    public class PartnerStatisticsEmailSentEvent : AccountEvent
    {
        public override string Name { get { return Administration.PartnerStatisticsEmailSentEventName; } }
        public override string IconPostfix { get { return Icon.Briefcase; } }
        public override string Message { get { return string.Format(Administration.PartnerStatisticsEmailSentEventMessage, RelatedUser.Partner.Id); } }
    }

    public class ExpertWidgetGeneratedEvent : AccountEvent
    {
        public override string Name { get { return Administration.ExpertWidgetGeneratedEventName; } }
        public override string IconPostfix { get { return Icon.ThLarge; } }
        public override string Message { get { return string.Format(Administration.ExpertWidgetGeneratedEventMessage, RelatedUser.Expert.Id); } }
    }

    public class NewChatEvent : AccountEvent
    {
        public override string Name { get { return Administration.NewChatEventName; } }
        public override string IconPostfix { get { return Icon.Briefcase; } }
        public override string Message { get { return string.Format(Administration.NewChatEventMessage, RelatedUser != null ? RelatedUser.Id.ToString() : Resources.Chat.Anonymous); } }
    }

    public class ExpertPublicDataChangedEvent : AccountEvent
    {
        public override string Name { get { return Administration.ExpertPublicDataChangedName; } }
        public override string IconPostfix { get { return Icon.Briefcase; } }
        public override string Message { get { return string.Format(Administration.ExpertPublicDataChangedMessage, RelatedUser.Expert.PublicName); } }
    }

    public class BecomePartnerRequestEvent : AccountEvent
    {
        public override string Name { get { return Administration.BecomePartnerRequestEventName; } }
        public override string IconPostfix { get { return Icon.Briefcase; } }
        public override string Message { get { return string.Format(Administration.BecomePartnerRequestEventMessage, RelatedUser.Email); } }
    }
}