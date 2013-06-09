using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Experts.Core.Entities
{
    public class SEOKeyword:IEntity
    {
        public SEOKeyword()
        {
            CategoryAttributes = new Collection<CategoryAttributeValue>();
            HitCount = 1;
        }

        public int Id { get; set; }
        public string Phrase { get; set; }
        public virtual Category Category { get; set; } 
        public virtual ICollection<CategoryAttributeValue> CategoryAttributes { get; private set; }
        public int HitCount { get; set; }

        public int IntType { get; set; }
        public SEOKeywordType Type
        {
            get { return (SEOKeywordType)IntType; }
            set { IntType = (int)value; }
        }

        public int IntStatus { get; set; }
        public SEOKeywordStatus Status 
        { 
            get { return (SEOKeywordStatus) IntStatus; }
            set { IntStatus = (int)value; }
        }

        public int IntSource { get; set; }
        public SEOKeywordSource Source
        {
            get { return (SEOKeywordSource)IntSource; }
            set { IntSource = (int)value; }
        }
    }

    public enum SEOKeywordSource
    {
        Automatic = 0,
        Manual = 1
    }

    public enum SEOKeywordType
    {
        Phrase = 0,
        Question = 1,
        Expert = 2
    }

    public enum SEOKeywordStatus
    {
        New = 0,
        Processed = 1,
        Blocked = 2,
        Undefined = 3
    }
}
