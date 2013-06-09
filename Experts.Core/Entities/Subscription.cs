using System;

namespace Experts.Core.Entities
{
    public class Subscription : IAuditableEntity
    {
        public int Id { get; set; }
        public string Contact { get; set; }
        public bool IsExpert { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
    }
}
