using System;
using System.ComponentModel.DataAnnotations;

namespace Experts.Core.Entities
{
    public class Feedback : IAuditableEntity
    {
        public int Id { get; set; }

        public FeedbackMark Mark
        {
            get { return (FeedbackMark) FeedbackMarkInt; }
            set { FeedbackMarkInt = (int) value; }
        }

        [Required]
        public int FeedbackMarkInt { get; set; }
        
        public string Content { get; set; }

        public string Comment { get; set; }

        [Required]
        public virtual Thread Thread { get; set; }

        [Required]
        public DateTime CreationDate {get; set; }

        [Required]
        public DateTime LastModificationDate {get; set; }
    }

    public enum FeedbackMark
    {
        Positive = 1,
        Neutral = 2,
        Negative = 3,
    }
}
