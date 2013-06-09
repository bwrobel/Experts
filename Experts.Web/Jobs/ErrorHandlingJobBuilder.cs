using System;
using Quartz;

namespace Experts.Web.Jobs
{
    public static class ErrorHandlingJobBuilder
    {
        public static IJobDetail Create<T>()
            where T:JobWithErrorHandling
        {
            return JobBuilder
                .Create<T>()
                .WithIdentity(Guid.NewGuid().ToString())
                .Build();
        }
    }
}