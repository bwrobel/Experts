using System;
using Resources;

namespace Experts.Core.Entities.Events
{
    public abstract class ExceptionEvent : Event
    {
    }

    public class SystemFailureEvent : ExceptionEvent
    {
        public override string Name { get { return Administration.SystemFailureEventName; } }
        public override string IconPostfix { get { return Icon.Wrench; } }
        public override string Message { get { var endlineIndex = Data.IndexOf(Environment.NewLine); return endlineIndex > 0 ? Data.Substring(0, endlineIndex) : Data; } }
    }

    public class UserFailureEvent : SystemFailureEvent
    {
        public override string IconPostfix { get { return Icon.User; } }
    }

    public class SystemBreakdownEvent : SystemFailureEvent
    {
        public override string Name { get { return Administration.SystemBreakdownEvent; } }
    }
}