using System.Web;
using Quartz;
using Quartz.Impl;

namespace Experts.Web.Jobs
{
    public class Scheduler
    {
        private static readonly IScheduler _scheduler;
        private static readonly ISchedulerFactory _schedulerFactory;

        static Scheduler()
        {
            _schedulerFactory = new StdSchedulerFactory();
            _scheduler = _schedulerFactory.GetScheduler();
            _scheduler.Start();   
        }

        public static IScheduler GetScheduler()
        {
            return _scheduler;
        }


        public static void Configure(HttpContext httpContext)
        {
            _scheduler.ScheduleJob(ErrorHandlingJobBuilder.Create<ThreadsOccupationReleaseJob>(), TriggerFactory.CreateMinuteTrigger());
            _scheduler.ScheduleJob(ErrorHandlingJobBuilder.Create<ActiveUsersRefreshJob>(), TriggerFactory.CreateMinuteTrigger());
            _scheduler.ScheduleJob(ErrorHandlingJobBuilder.Create<ThreadsAcceptanceJob>(), TriggerFactory.CreateHourTrigger());
            _scheduler.ScheduleJob(ErrorHandlingJobBuilder.Create<EmailSendingJob>(), TriggerFactory.CreateMinuteTrigger());
            //_scheduler.ScheduleJob(ErrorHandlingJobBuilder.Create<AwaitingExpertResponseEmailJob>(), TriggerFactory.CreateCustomMinuteTrigger(10));
            //_scheduler.ScheduleJob(ErrorHandlingJobBuilder.Create<ExpertStatisticsEmailJob>(), TriggerFactory.CreateCustomHourTrigger(12));
            //_scheduler.ScheduleJob(ErrorHandlingJobBuilder.Create<AcceptAnswerReminderEmailJob>(), TriggerFactory.CreateCustomHourTrigger(12));
            //_scheduler.ScheduleJob(ErrorHandlingJobBuilder.Create<NoExpertResponseEmailJob>(), TriggerFactory.CreateCustomHourTrigger(2));
            //_scheduler.ScheduleJob(ErrorHandlingJobBuilder.Create<MonthlyAskEncouragementEmailJob>(), TriggerFactory.CreateHourTrigger());
            //_scheduler.ScheduleJob(ErrorHandlingJobBuilder.Create<PartnerStatisticsEmailJob>(), TriggerFactory.CreateCustomDayTrigger(1));
            //_scheduler.ScheduleJob(ErrorHandlingJobBuilder.Create<StatisticsEventJob>(), TriggerFactory.CreateHourTrigger());
            _scheduler.ScheduleJob(ErrorHandlingJobBuilder.Create<TemporaryAttachmentFolderJob>(), TriggerFactory.CreateHourTrigger());
        }
    }
}