using System;
using System.Collections.Generic;
using System.Configuration;
using log4net;

namespace Experts.Core.Logging.Log4NetLog
{
    public class Log4NetLog : ILog
    {
        public string ApplicationName { get; set; }
        private static readonly Dictionary<string, log4net.ILog> Loggers = new Dictionary<string, log4net.ILog>();
        private static readonly object Lock = new object();
        private static log4net.ILog GetLogger(string source)
        {
            if(source == null) throw new ConfigurationErrorsException("Log Type is obligatory","source",0);

            lock (Lock)
            {
                if (Loggers.ContainsKey(source))
                {
                    return Loggers[source];
                }
                var logger = LogManager.GetLogger(source);
                Loggers.Add(source, logger);
                return logger;
            }
        }

        public Log4NetLog(string applicationName)
        {
            ApplicationName = applicationName;
        }

        public void Debug(string source, string message, params object[] args)
        {
            var log = GetLogger(source);    
            new Log4NetEntry(log.Debug, log.IsDebugEnabled,ApplicationName,message,args)
                .Proceed();
        }

        public void Info(string source, string message, params object[] args)
        {
            var log = GetLogger(source);
            new Log4NetEntry(log.Info, log.IsDebugEnabled, ApplicationName, message, args)
                .Proceed();
        }

        public void Warn(string source, string message, params object[] args)
        {
            var log = GetLogger(source);
            new Log4NetEntry(log.Warn, log.IsDebugEnabled, ApplicationName, message, args)
                .Proceed();
        }

        public void Warn(string source, Exception exception, string message, params object[] args)
        {
            var log = GetLogger(source);
            new Log4NetEntry(log.Warn, log.IsDebugEnabled, ApplicationName, message, args)
                .WithException(exception)
                .Proceed();
        }

        public void Error(string source, string message, params object[] args)
        {
            var log = GetLogger(source);
            new Log4NetEntry(log.Error, log.IsDebugEnabled, ApplicationName, message, args)
                .Proceed();
        }

        public void Error(string source, Exception exception, string message, params object[] args)
        {
            var log = GetLogger(source);
            new Log4NetEntry(log.Error, log.IsDebugEnabled, ApplicationName, message, args)
                .WithException(exception)
                .Proceed();
        }

        public void Fatal(string source, string message, params object[] args)
        {
            var log = GetLogger(source);
            new Log4NetEntry(log.Fatal, log.IsDebugEnabled, ApplicationName, message, args)
                .Proceed();
        }

        public void Fatal(string source, Exception exception, string message, params object[] args)
        {
            var log = GetLogger(source);
            new Log4NetEntry(log.Fatal, log.IsDebugEnabled, ApplicationName, message, args)
                .WithException(exception)
                .Proceed();
        }



        public void Debug(Type source, string message, params object[] args)
        {
            Fatal(source.FullName, message, args);
        }

        public void Info(Type source, string message, params object[] args)
        {
            Info(source.FullName, message, args);
        }

        public void Warn(Type source, string message, params object[] args)
        {
            Warn(source.FullName, message, args);
        }

        public void Warn(Type source, Exception exception, string message, params object[] args)
        {
            Warn(source.FullName, exception, message, args);
        }

        public void Error(Type source, string message, params object[] args)
        {
            Error(source.FullName, message, args);
        }

        public void Error(Type source, Exception exception, string message, params object[] args)
        {
            Error(source.FullName, exception, message, args);
        }

        public void Fatal(Type source, string message, params object[] args)
        {
            Fatal(source.FullName, message, args);
        }

        public void Fatal(Type source, Exception exception, string message, params object[] args)
        {
            Fatal(source.FullName, exception, message, args);
        }
    }
}