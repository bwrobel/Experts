using System;
using System.Linq;
using Experts.Core.Entities;
using Experts.Core.Repositories;
using Experts.Core.Utils;
using Experts.Web.Helpers;
using Experts.Web.Utils;
using Quartz;
using Experts.Core.Entities.Events;

namespace Experts.Web.Jobs
{
    public class AwaitingExpertResponseEmailJob : JobWithErrorHandling
    {
        public override void DoJob(IJobExecutionContext context)
        {
            var repository = RepositoryHelper.Repository;

            var awaitingThreads = repository.Thread.Find(query: q => q
                .ByElapsedHours(1)
                .ByNotOccupied()
                .ByNotificationStatus(false));

            foreach (var thread in awaitingThreads)
            {
                EventLog.Event<AwaitingExpertResponseEvent>(thread);
                Email.Send<AwaitingExpertResponseEmail>(thread);
                repository.Thread.AddPost(thread, SystemPostFactory.BuildAwaitingExpertResponsePost(repository.Consultant.All().First()));
                thread.IsNotified = true;
                repository.Thread.Update(thread);
            }
        }
    }

}