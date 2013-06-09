using System.Collections.Generic;

namespace Experts.Core.Entities
{
    public class Opinion : IEntity
    {
        public Opinion()
        {
            Categories = new List<Category>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string OpinionContent { get; set; }

        public string AddressCity { get; set; }

        public bool IsGeneral { get; set; }

        public virtual ICollection<Category> Categories { get; private set; }

        public OpinionMark OpinionMark
        {
            get { return (OpinionMark)OpinionInt; }
            set { OpinionInt = (int)value; }
        }

        public int OpinionInt { get; set; }

        public AuthorType AuthorType
        {
            get { return (AuthorType)AuthorTypeInt; }
            set { AuthorTypeInt = (int)value; }
        }

        public int AuthorTypeInt { get; set; }
    }

    public enum OpinionMark
    {
        Positive = 1,
        Negative = 2
    }

    public enum AuthorType
    {
        User = 1,
        Expert = 2
    }
}
