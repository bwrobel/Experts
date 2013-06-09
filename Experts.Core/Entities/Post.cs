using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Experts.Core.Entities
{
    public class Post : IAuditableEntity
    {
        public Post()
        {
            IsPubliclyVisible = true;
            Attachments = new Collection<Attachment>();
        }

        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public string PublicContent { get; set; }

        public bool IsPubliclyVisible { get; set; }

        public virtual User Author { get; set; }

        public virtual Thread Thread { get; private set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModificationDate { get; set; }

        public PostType Type
        {
            get { return (PostType)IntType; }
            set { IntType = (int)value; }
        }

        [Required]
        public int IntType  { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }

        public bool IsReadOnly { get; set; }

        public string GetPublicNameIfNotEmpty()
        {
            return PublicContent ?? Content;
        }
    }

    public enum PostType
    {
        Question = 1,
        Answer = 2,
        DetailsRequest = 3,
        Details = 4,
        GiveUp = 5,
        Attachment = 6,
        Reserved = 7,
        Released = 8,
        Analyzing = 9,
        Answered = 10,
        ModeratorAnswer = 11,
        ExpertPM = 12,
        Info = 13,
        Hidden = 14
    }
}
