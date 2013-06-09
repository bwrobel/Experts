using System;
using Experts.Core.Entities;
using Experts.Core.Repositories;
using Experts.Core.Utils;
using Quartz;
using System.Linq;

namespace Experts.Web.Jobs
{
    public class ThreadsOccupationReleaseJob : JobWithErrorHandling
    {
        public override void DoJob(IJobExecutionContext context)
        {
            var repository = RepositoryHelper.Repository;

            var threadsToRelease = repository.Thread.Find(query: q => q.ByExpertReleaseDateExpiration());
            foreach (var thread in threadsToRelease)
            {
                if (thread.State == ThreadState.Occupied)
                {
                    thread.Expert = null;
                    thread.State = ThreadState.Unoccupied;
                    thread.AdditionalExpertProvisionValue = 0;

                    if (thread.Expert != null)
                    {
                        thread.Expert = null;
                        repository.Thread.Update(thread);
                    }
                }
                thread.ExpertReleaseDate = null;

                repository.Thread.Update(thread);

                repository.Thread.DeletePost(thread.Posts.Single(p => p.Type == PostType.Analyzing));
            }
        }
    }

}