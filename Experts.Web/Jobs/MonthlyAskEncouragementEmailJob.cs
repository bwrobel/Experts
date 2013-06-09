using System;
using Experts.Core.Entities;
using Experts.Core.Utils;
using Experts.Web.Helpers;
using Quartz;

namespace Experts.Web.Jobs
{
    public class MonthlyAskEncouragementEmailJob : JobWithErrorHandling
    {
        public override void DoJob(IJobExecutionContext context)
        {
            var repository = RepositoryHelper.Repository;

            var users = repository.User.All();

            foreach(var user in users)
            {
                var dateDifference = user.LastAskEncouragementDate.HasValue ? DateTime.Now - user.LastAskEncouragementDate : null;
                var creationDateDifference = DateTime.Now - user.CreationDate;

                if ((dateDifference == null || dateDifference.Value.TotalDays > 30) && creationDateDifference.TotalDays > 20)
                {
                    Email.Send<MonthlyAskEncouragementEmail>(user);
                    user.LastAskEncouragementDate = DateTime.Now;
                    repository.User.Update(user);
                }
            }
        }
    }

}