using System;
using Experts.Core.Entities;
using Experts.Core.Entities.Events;
using Experts.Core.Repositories;
using Experts.Core.Utils;
using Experts.Web.Helpers;
using Quartz;

namespace Experts.Web.Jobs
{
    public class ThreadsAcceptanceJob : JobWithErrorHandling
    {
        public override void DoJob(IJobExecutionContext context)
        {
            var repository = RepositoryHelper.Repository;
            
            var threadsToAccept = repository.Thread.Find(query: q => q.ByThreadWaitForAcceptance());
            foreach (var thread in threadsToAccept)
            {
                if (thread.State == ThreadState.Accepted)
                {
                    thread.State = ThreadState.Closed;
                    Log.Event<ThreadClosedEvent>(thread);
                }
                repository.Thread.Update(thread);
            }
        }

    }

}