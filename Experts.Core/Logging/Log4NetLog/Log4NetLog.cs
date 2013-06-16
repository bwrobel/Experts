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
        private string _source;
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

        public ILog SetSource(Type source)
        {
           return SetSource(source.FullName);
        }

        public ILog SetSource(string source)
        {
            _source = source;
            return this;
        }

        public void Debug(string message, params object[] args)
        {
            var log = GetLogger(_source);    
            new Log4NetEntry(log.Debug, log.IsDebugEnabled,ApplicationName,message,args)
                .Proceed();
        }

        public void Info(string message, params object[] args)
        {
            var log = GetLogger(_source);
            new Log4NetEntry(log.Info, log.IsDebugEnabled, ApplicationName, message, args)
                .Proceed();
        }

        public void Warn(string message, params object[] args)
        {
            var log = GetLogger(_source);
            new Log4NetEntry(log.Warn, log.IsDebugEnabled, ApplicationName, message, args)
                .Proceed();
        }

        public void Warn(Exception exception, string message, params object[] args)
        {
            var log = GetLogger(_source);
            new Log4NetEntry(log.Warn, log.IsDebugEnabled, ApplicationName, message, args)
                .WithException(exception)
                .Proceed();
        }

        public void Error(string message, params object[] args)
        {
            var log = GetLogger(_source);
            new Log4NetEntry(log.Error, log.IsDebugEnabled, ApplicationName, message, args)
                .Proceed();
        }

        public void Error(Exception exception, string message, params object[] args)
        {
            var log = GetLogger(_source);
            new Log4NetEntry(log.Error, log.IsDebugEnabled, ApplicationName, message, args)
                .WithException(exception)
                .Proceed();
        }

        public void Fatal(string message, params object[] args)
        {
            var log = GetLogger(_source);
            new Log4NetEntry(log.Fatal, log.IsDebugEnabled, ApplicationName, message, args)
                .Proceed();
        }

        public void Fatal(Exception exception, string message, params object[] args)
        {
            var log = GetLogger(_source);
            new Log4NetEntry(log.Fatal, log.IsDebugEnabled, ApplicationName, message, args)
                .WithException(exception)
                .Proceed();
        }
    }
}