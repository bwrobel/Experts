using Experts.Core.Entities;
using Experts.Web.Models.Forms;

namespace Experts.Web.Models.Threads
{
    public class Question
    {
        public ThreadForm ThreadForm { get; set; }
        public Category Category { get; set; }
    }
}