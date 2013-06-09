using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Experts.Core.Entities
{
    public class Chat : IAuditableEntity
    {
        public Chat()
        {
            Messages = new Collection<ChatMessage>();
        }
        public int Id { get; set; }

        public virtual ICollection<ChatMessage> Messages { get; set; }

        public bool IsClosed { get; set; }

        public bool IsSummarySent { get; set; }

        public virtual User Owner { get; set; }

        public string OwnerEmail { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModificationDate { get; set; }

        public DateTime LastReadTime { get; set; }

        public virtual User Moderator { get; set; }
    }
}
