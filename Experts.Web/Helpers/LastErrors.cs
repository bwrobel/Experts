using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Experts.Core.Entities.Events;

namespace Experts.Web.Helpers
{
    public static class ErrorsHelper
    {
        private static IList<Tuple<DateTime, Exception>> _lifetimeErrors = new List<Tuple<DateTime, Exception>>();
        private static readonly IList<Exception> LastRepeteableErrors = new List<Exception>();


        public static int LogApplicationException(Exception exception)
        {
            var eventId = EventLog.Event<SystemFailureEvent>(exception);

            AddToLastErrors(exception);

            if(IsANewRepeteableError(exception, int.Parse(ConfigurationManager.AppSettings["Events.BreakdownOccurencePerMinute"])))
                EventLog.Event<SystemBreakdownEvent>(exception);

            return eventId;
        }

        private static IList<Tuple<DateTime, Exception>> LastMinuteErrors
        {
            get
            {
                return _lifetimeErrors = _lifetimeErrors.Where(i => DateTime.Now - i.Item1 < TimeSpan.FromMinutes(1)).ToList(); ;
            }
        }

        private static bool IsANewRepeteableError(Exception exception, int minimalCount)
        {
            var errors = LastMinuteErrors.ToList();
            var result = errors.Count(i => i.Item2.StackTrace == exception.StackTrace) >= minimalCount && LastRepeteableErrors.All(e => e.StackTrace != exception.StackTrace);

            if(result)
                LastRepeteableErrors.Add(exception);

            return result;
        }

        private static void AddToLastErrors(Exception exception)
        {
            var item = new Tuple<DateTime, Exception>(DateTime.Now, exception);
            LastMinuteErrors.Add(item);
        }
    }
}
