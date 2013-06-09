using System.Collections.Generic;
using Experts.Core.Entities;
using Experts.Core.Entities.Events;

namespace Experts.Web.Models.Threads
{
    public class ThreadDetailsModel
    {
        public Thread Thread { get; set; }
        public bool IsSanitizationMode { get; set; }
        public bool IsModerationMode { get; set; }
        public PostFormModel PostFormModel { get; set; }
        public IEnumerable<Event> ThreadEvents { get; set; }

        public ThreadDetailsModel(Thread thread)
        {
            Thread = thread;
        }

        public ThreadDetailsModel()
        {}
    }
}