using System;
using log4net;

namespace Experts.Core.Logging.Log4NetLog
{
    public class Log4NetEntry : ILogEntry
    {
        private readonly string _applicationName;
        private readonly LoggerCall _loggerCall;
        private readonly bool _enabled;
        private readonly string _message;
        private Exception _exception;
        
        internal Log4NetEntry(LoggerCall loggerCall, bool enabled, string applicationName,string message, params object[] args)
        {
            _loggerCall = loggerCall;
            _enabled = enabled;
            _applicationName = applicationName;

            try{_message = string.Format(message, args);}
            catch (Exception){_message = string.Format("Faulty parsed message \"{0}\"", message);}
        }

        internal delegate void LoggerCall(object message, Exception exception);

        private void ProcessLogEntry()
        {
            if (!_enabled) return;
            
            PreProcessLogEntry();

            _loggerCall(_message, _exception);
            
            PostProcessLogEntry();
        }

        private void PreProcessLogEntry()
        {
            NDC.Push(_applicationName);
        }

        private void PostProcessLogEntry()
        {
            NDC.Pop();
        }

        public ILogEntry WithException(Exception ex)
        {
            _exception = ex;
            return this;
        }

        public void Proceed()
        {
            ProcessLogEntry();
        }
    }
}