using System;
using Quartz;

namespace Experts.Web.Jobs
{
    public class TriggerFactory
    {
        public static ITrigger CreateMinuteTrigger()
        {
            return TriggerBuilder.Create()
                .WithIdentity("trigger-" + Guid.NewGuid())
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever())
                .Build();    
        }

        public static ITrigger CreateHourTrigger()
        {
            return TriggerBuilder.Create()
                .WithIdentity("trigger-" + Guid.NewGuid())
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInHours(1).RepeatForever())
                .Build();
        }

        public static ITrigger CreateCustomDayTrigger(int days)
        {
            return TriggerBuilder.Create()
                .WithIdentity("trigger-" + Guid.NewGuid())
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInHours(days*24).RepeatForever())
                .Build();
        }

        public static ITrigger CreateCustomMinuteTrigger(int minutes)
        {
            return TriggerBuilder.Create()
                .WithIdentity("trigger-" + Guid.NewGuid())
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(minutes).RepeatForever())
                .Build();
        }

        public static ITrigger CreateCustomHourTrigger(int hours)
        {
            return TriggerBuilder.Create()
                .WithIdentity("trigger-" + Guid.NewGuid())
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInHours(hours).RepeatForever())
                .Build();
        }
    }

}