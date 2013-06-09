using System;
using Experts.Web.Helpers;
using Quartz;

namespace Experts.Web.Jobs
{
    public class ActiveUsersRefreshJob : JobWithErrorHandling
    {
        public override void DoJob(IJobExecutionContext context)
        {
            ActiveUsersHelper.RemoveInactiveUsers();
        }


    }
}