using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Experts.Core.Entities;

namespace Experts.Web.Models.Profile
{
    public class MyQuestions
    {
        public IEnumerable<Thread> CurrentThreads { get; set; }
        public IEnumerable<Thread> AcceptedThreads { get; set; }
    }
}