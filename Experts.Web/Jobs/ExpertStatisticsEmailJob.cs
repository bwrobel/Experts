using System;
using Experts.Core.Entities;
using Experts.Core.Utils;
using Experts.Web.Helpers;
using Quartz;

namespace Experts.Web.Jobs
{
    public class ExpertStatisticsEmailJob : JobWithErrorHandling
    {
        public override void DoJob(IJobExecutionContext context)
        {
            if (DateTime.Now.Day == 1)
            {
                var repository = RepositoryHelper.Repository;

                var experts = repository.Expert.All();

                var currentMonthName = MonthNamesHelper.PolishMonthNames(StatisticsHelper.GetLastMonthNumber());

                foreach (var expert in experts)
                {
                    var expertMonthlyStatistics = repository.Expert.GetExpertMonthlyStatistics(expert.Id);
                    Email.Send<ExpertStatisticsEmail>(expert, currentMonthName, expertMonthlyStatistics);
                }
            }
        }
    }

}