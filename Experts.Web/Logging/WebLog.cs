using System;
using Experts.Core.Logging;
using Experts.Core.Logging.Log4NetLog;
using Experts.Web.Helpers;

namespace Experts.Web.Logging
{
    public class WebLog : ILog
    {
        private readonly ILog _log = Log4NetLogFactory.CreateNew();
        private readonly IHttpContextHelper _httpContextHelper = new HttpContextHelper();
        private string GetMessage(string message)
        {
            return string.Format("URL:{0};SESSION_ID:{1};{2}", 
                _httpContextHelper.RequestUrl, _httpContextHelper.SessionId, message); 
        }

        public void Debug(Type source, string message, params object[] args)
        {
            _log.Debug(source, GetMessage(message), args);
        }

        public void Debug(string source, string message, params object[] args)
        {
            _log.Debug(source, GetMessage(message), args);
        }

        public void Info(Type source, string message, params object[] args)
        {
            _log.Info(source, GetMessage(message), args);
        }

        public void Info(string source, string message, params object[] args)
        {
            _log.Info(source, GetMessage(message), args);
        }

        public void Warn(Type source, string message, params object[] args)
        {
            _log.Warn(source, GetMessage(message), args);
        }

        public void Warn(string source, string message, params object[] args)
        {
            _log.Warn(source, GetMessage(message), args);
        }

        public void Warn(Type source, Exception exception, string message, params object[] args)
        {
            _log.Warn(source, exception, GetMessage(message), args);
        }

        public void Warn(string source, Exception exception, string message, params object[] args)
        {
            _log.Warn(source, exception, GetMessage(message), args);
        }

        public void Error(Type source, string message, params object[] args)
        {
            _log.Error(source, GetMessage(message), args);
        }

        public void Error(string source, string message, params object[] args)
        {
            _log.Error(source, GetMessage(message), args);
        }

        public void Error(Type source, Exception exception, string message, params object[] args)
        {
            _log.Error(source, exception, GetMessage(message), args);
        }

        public void Error(string source, Exception exception, string message, params object[] args)
        {
            _log.Error(source, exception, GetMessage(message), args);
        }

        public void Fatal(Type source, string message, params object[] args)
        {
            _log.Fatal(source, GetMessage(message), args);
        }

        public void Fatal(string source, string message, params object[] args)
        {
            _log.Fatal(source, GetMessage(message), args);
        }

        public void Fatal(Type source, Exception exception, string message, params object[] args)
        {
            _log.Fatal(source, exception, GetMessage(message), args);
        }

        public void Fatal(string source, Exception exception, string message, params object[] args)
        {
            _log.Fatal(source, exception, GetMessage(message), args);
        }
    }
}