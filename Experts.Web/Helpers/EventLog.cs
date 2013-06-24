using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using Experts.Core.Entities;
using Experts.Core.Entities.Events;
using Experts.Core.Logging;
using Experts.Core.Logging.Log4NetLog;
using Experts.Core.Utils;
using System.Web;
using Experts.Web.Models.Events;

namespace Experts.Web.Helpers
{
    public static class EventLog
    {
        private static ILog _log = Log4NetLogFactory.CreateNew();

        public static TEventType CreateEvent<TEventType>()
            where TEventType : Event, new()
        {
            
            return new TEventType { IsHandled = GetReactionType(typeof(TEventType)) == null };
        }

        public static void Event<TEventType>(string data)
            where TEventType : Event, new()
        {
            var logEvent = CreateEvent<TEventType>();
            logEvent.Data = data;

            RepositoryHelper.Repository.Event.Add(logEvent);
        }

        public static void Event<TEventType>(User relatedUser = null, string additionalData = null, int? additionalId = null)
            where TEventType : AccountEvent, new()
        {
            relatedUser = relatedUser ?? AuthenticationHelper.CurrentUser;

            var logEvent = CreateEvent<TEventType>();
            logEvent.RelatedUser = relatedUser;
            logEvent.Data = additionalData;
            logEvent.AdditionalId = additionalId ?? 0;

            _log.Info(typeof(EventLog),logEvent.ToString());
            RepositoryHelper.Repository.Event.Add(logEvent);
        }

        public static void Event<TEventType>(Thread relatedThread, string additionalData = null, int? additionalId = null)
            where TEventType : ThreadEvent, new()
        {
            var logEvent = CreateEvent<TEventType>();
            logEvent.RelatedThread = relatedThread;
            logEvent.Data = additionalData;
            logEvent.AdditionalId = additionalId ?? 0;

            _log.Info(typeof(EventLog), logEvent.ToString());
            RepositoryHelper.Repository.Event.Add(logEvent);
        }

        public static void Event<TEventType>(Payment relatedPayment)
            where TEventType : PaymentEvent, new()
        {
            var logEvent = CreateEvent<TEventType>();
            logEvent.RelatedPayment = relatedPayment;

            _log.Info(typeof(EventLog), logEvent.ToString());
            RepositoryHelper.Repository.Event.Add(logEvent);
        }

        public static int Event<TEventType>(Exception exception)
            where TEventType : ExceptionEvent, new()
        {
            var data = GetExceptionData(exception);

            var storeFailed = false;

            try
            {
                var logEvent = CreateEvent<TEventType>();
                logEvent.Data = data;

                _log.Error(typeof(EventLog), logEvent.ToString());
                RepositoryHelper.Repository.Event.Add(logEvent);
                return logEvent.Id;
            }
            catch (Exception)
            {
                storeFailed = true;
            }
            finally
            {
                WriteToSystemLog(data, storeFailed ? EventLogEntryType.Error : EventLogEntryType.Warning);
            }

            return 0;
        }

        public static void ExpertQualificationChangedEvent(User user, string message)
        {
            var lastEvent = RepositoryHelper.Repository.Event.GetLastExpertQualificationsChangedEvent(user.Expert.Id);

            if (lastEvent == null)
            {
                Event<ExpertQualificationsChangedEvent>(user, message);
            }
            else
            {
                lastEvent.Data += "<br/>" + message;

                _log.Info(typeof(EventLog), lastEvent.ToString());
                RepositoryHelper.Repository.Event.Update(lastEvent);
            }
        }

        private static void WriteToSystemLog(string message, EventLogEntryType entryType)
        {
            /*Trzeba zarejestrować źródło:
             * w cmd jako admin: eventcreate /ID 1 /L APPLICATION /T INFORMATION /SO Experts /D “Registering”
            */

            const string sourceName = "Experts";
            //EventLog.WriteEntry(sourceName, message, entryType);
        }

        private static string GetExceptionData(Exception exception)
        {
            var data = new StringBuilder();

            data.AppendLine(exception.Message);
            data.AppendLine();


            if (exception.InnerException != null)
                data.AppendLine("Inner exception: " + exception.InnerException.Message);

            if (AuthenticationHelper.CurrentUser != null)
                data.AppendLine("Logged user id: " + AuthenticationHelper.CurrentUser.Id);


            if (HttpContext.Current != null)
            {
                data.AppendLine("Request url: " + HttpContext.Current.Request.RawUrl);
                data.AppendLine("Request method: " + HttpContext.Current.Request.HttpMethod);

                var formParameters = new StringBuilder();
                var formArray = HttpContext.Current.Request.Form;

                foreach (var paramKey in formArray.AllKeys)
                    formParameters.AppendLine(paramKey + " => " + formArray[paramKey]);

                if (formParameters.Length > 0)
                    data.AppendLine("Request form:" + Environment.NewLine + formParameters);
            }


            data.AppendLine("Source: " + exception.Source);
            data.AppendLine("Stack trace:" + Environment.NewLine + exception.StackTrace);

            return data.ToString();
        }

        public static Type GetReactionType(Type eventType)
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof (EventReactionModel<>))
                .SingleOrDefault(t => t.BaseType.GetGenericArguments().First().IsAssignableFrom(eventType));
        }
    }
}