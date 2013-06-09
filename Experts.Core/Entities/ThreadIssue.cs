using System;
using System.ComponentModel.DataAnnotations;

namespace Experts.Core.Entities
{
    public class ThreadIssue : IAuditableEntity
    {
        public int Id { get; set; }

        public ThreadIssueType IssueType
        {
            get { return (ThreadIssueType) IntIssueType; }
            set { IntIssueType = (int) value; }
        }

        public string Comment { get; set; }

        [Required]
        public virtual Thread Thread { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public DateTime LastModificationDate { get; set; }

        public int IntIssueType { get; set; }
    }

    public enum ThreadIssueType
    {
        InvalidCategory = 1,
        Duplicate = 2,
        Other = 3
    }
}
