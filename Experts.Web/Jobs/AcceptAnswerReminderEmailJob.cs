using System;
using Experts.Core.Entities;
using Experts.Core.Repositories;
using Experts.Core.Utils;
using Experts.Web.Helpers;
using Quartz;
using Experts.Core.Entities.Events;

namespace Experts.Web.Jobs
{
    public class AcceptAnswerReminderEmailJob : JobWithErrorHandling
    {
        public override void DoJob(IJobExecutionContext context)
        {
            var repository = RepositoryHelper.Repository;

            var elapsedThreads = repository.Thread.Find(query: q => q.ByElapsedHours(48).ByOutgoingStates());
            foreach (var thread in elapsedThreads)
            {
                EventLog.Event<AcceptAnswerReminderEvent>(thread);
                Email.Send<AcceptAnswerReminderEmail>(thread);
            }
        }

    }

}