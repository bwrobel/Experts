using System;
using System.Collections.Generic;
using System.Configuration;
using log4net;

namespace Experts.Core.Logging.Log4NetLog
{
    public class Log4NetLog : ILog
    {
        public string ApplicationName { get; set; }
        private static readonly Dictionary<Type, log4net.ILog> Loggers = new Dictionary<Type, log4net.ILog>();
        private static readonly object Lock = new object();
        private Type _source;
        private static log4net.ILog GetLogger(Type source)
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
           _source = source;
           return this;
        }

        public ILogEntry Debug(string message, params object[] args)
        {
            var log = GetLogger(_source);    
            return new Log4NetEntry(log.Debug, log.IsDebugEnabled,ApplicationName,message,args);
        }

        public ILogEntry Info(string message, params object[] args)
        {
            var log = GetLogger(_source);
            return new Log4NetEntry(log.Info, log.IsDebugEnabled, ApplicationName, message, args);
        }

        public ILogEntry Warn(string message, params object[] args)
        {
            var log = GetLogger(_source);
            return new Log4NetEntry(log.Warn, log.IsDebugEnabled, ApplicationName, message, args);
        }

        public ILogEntry Error(string message, params object[] args)
        {
            var log = GetLogger(_source);
            return new Log4NetEntry(log.Error, log.IsDebugEnabled, ApplicationName, message, args);
        }

        public ILogEntry Fatal(string message, params object[] args)
        {
            var log = GetLogger(_source);
            return new Log4NetEntry(log.Fatal, log.IsDebugEnabled, ApplicationName, message, args);
        }
    }
}