using Resources;

namespace Experts.Core.Entities.Events
{
    public abstract class PaymentEvent : Event
    {
        public Payment RelatedPayment { get; set; }
    }

    public class PaymentFailedEvent : PaymentEvent
    {
        public override string Name { get { return Administration.PaymentFailedEventName; } }
        public override string IconPostfix { get { return Icon.User; } }
        public override string Message { get { return string.Format(Administration.PaymentFailedEventMessage, Data, RelatedPayment.Id); } }
    }
}
