using System;
using Experts.Core.Entities;
using Experts.Core.Entities.Events;
using Experts.Core.Utils;
using Experts.Web.Helpers;
using Quartz;

namespace Experts.Web.Jobs
{
    public class PartnerStatisticsEmailJob : JobWithErrorHandling
    {
        public override void DoJob(IJobExecutionContext context)
        {
            if (DateTime.Now.Day == 1)
            {
                var repository = RepositoryHelper.Repository;
                var partners = repository.Partner.All();

                foreach (var partner in partners)
                {
                    Email.Send<PartnerStatisticsEmail>(partner);
                    EventLog.Event<PartnerStatisticsEmailSentEvent>(partner.User);
                }
            }
        }
    }
}