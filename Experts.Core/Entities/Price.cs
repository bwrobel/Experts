using System.ComponentModel.DataAnnotations;

namespace Experts.Core.Entities
{
    public class Price : IEntity
    {
        public int Id { get; set; }

        public decimal Value { get; set; }

        [Required]
        public virtual Category Category { get; set; }

        public ThreadPriority Priority
        {
            get { return (ThreadPriority) IntPriority; }
            set { IntPriority = (int) value; }
        }

        public ThreadVerbosity Verbosity
        {
            get { return (ThreadVerbosity) IntVerbosity; }
            set { IntVerbosity = (int) value; }
        }

        public int IntPriority { get; set; }

        public int IntVerbosity { get; set; }
    }

    
}
