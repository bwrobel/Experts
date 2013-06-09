using System;
using Experts.Web.Helpers;
using Quartz;

namespace Experts.Web.Jobs
{
    public class EmailSendingJob : JobWithErrorHandling
    {
        public override void DoJob(IJobExecutionContext context)
        {      
            Email.RunMailQueue();
        }
    }
}