using System;

namespace Experts.Core.Entities.Events
{
    public abstract class Event : IEntity
    {
        protected Event()
        {
            OccurenceDate = DateTime.Now;
        }

        public int Id { get; set; }
        public DateTime OccurenceDate { get; set; }
        public bool IsHandled { get; set; }
        public User HandledBy { get; set; }
        public bool HideOnMainList { get; set; }

        public abstract string Name { get; }
        public abstract string IconPostfix { get; }
        public abstract string Message { get; }

        public virtual string Data { get; set; }
        public virtual int AdditionalId { get; set; }

        public override string ToString()
        {
            return string.Format("Event:{0};Data:{1};AdditionalId: {2}", GetType(), Data, AdditionalId);
        }
    }

    public static class Icon
    {
        public const string ExclamationSign = "exclamation-sign";
        public const string PlusSign = "plus-sign";
        public const string Wrench = "wrench";
        public const string User = "user";
        public const string Check = "check";
        public const string Briefcase = "briefcase";
        public const string Stop = "stop";
        public const string Play = "play";
        public const string Tag = "tag";
        public const string Tasks = "tasks";
        public const string Ok = "ok";
        public const string ThLarge = "th-large";
    }
}
