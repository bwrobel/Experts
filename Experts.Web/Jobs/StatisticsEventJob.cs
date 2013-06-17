using System;
using Experts.Core.Entities;
using Experts.Core.Entities.Events;
using Experts.Core.Repositories;
using Experts.Core.Utils;
using Experts.Web.Helpers;
using Quartz;

namespace Experts.Web.Jobs
{
    public class StatisticsEventJob : JobWithErrorHandling
    {
        public override void DoJob(IJobExecutionContext context)
        {
            SaveActiveUsersStatistics();

            SaveSEOKeywordsStatistics();

            if (DateTime.Now.Hour == 1)
                SaveLastDayThreadsStatistics();
        }

        private void SaveActiveUsersStatistics()
        {
            EventLog.Event<ActiveUsersStatisticsEvent>(ActiveUsersHelper.ActiveUsersCount.ToString());
            EventLog.Event<ActiveExpertStatisticsEvent>(ActiveUsersHelper.ActiveExpertsCount.ToString());
        }

        private void SaveSEOKeywordsStatistics()
        {
            var newSEOKeywordsCount = RepositoryHelper.Repository.SEOKeyword.Count(k => k.ByStatus(SEOKeywordStatus.New));
            EventLog.Event<NewSeoKeywordsStatisticsEvent>(newSEOKeywordsCount.ToString());
        }

        private void SaveLastDayThreadsStatistics()
        {
            var to = DateTime.Now.Subtract(DateTime.Now.TimeOfDay);
            var from = to.AddDays(-1);
            var lastDayThreadsCount = RepositoryHelper.Repository.Thread.Count(t => t.CreationDate >= from && t.CreationDate < to);
            EventLog.Event<LastDayThreadsStatisticsEvent>(lastDayThreadsCount.ToString());
        }
    }
}