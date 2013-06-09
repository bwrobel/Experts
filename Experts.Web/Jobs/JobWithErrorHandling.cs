using System;
using Experts.Web.Helpers;
using Quartz;

namespace Experts.Web.Jobs
{
    public abstract class JobWithErrorHandling : IJob
    {
        public abstract void DoJob(IJobExecutionContext context);

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                DoJob(context);
            }
            catch (Exception exception)
            {
                ErrorsHelper.LogApplicationException(exception);
                throw;
            }
        }
    }
}