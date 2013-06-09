using System.ComponentModel.DataAnnotations;

namespace Experts.Core.Entities
{
    public class Attachment : IEntity
    {
        public int Id { get; set; }

        public string AttachmentName { get; set; }

        public string AttachmentPath { get; set; }

        public int AttachmentSize { get; set; }

        public virtual Post Post { get; private set; }

        public virtual User Author { get; set; }

        public AttachmentType Type
        {
            get { return (AttachmentType)IntAttachmentType; }
            set { IntAttachmentType = (int)value; }
        }

        [Required]
        public int IntAttachmentType { get; set; }
        public string ContentType { get; set; }
    }

    public enum AttachmentType
    {
        Image = 1,
        Document = 2,
        Audio = 3,
        Video = 4,
        Hidden = 5
    }
}
