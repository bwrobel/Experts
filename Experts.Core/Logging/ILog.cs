using System;

namespace Experts.Core.Logging
{
    public interface ILog
    {
        ILog SetSource(Type source);
        ILog SetSource(string source);
        
        void Debug(string message, params object[] args);

        void Info(string message, params object[] args);

        void Warn(string message, params object[] args);
        void Warn(Exception exception, string message, params object[] args);

        void Error(string message, params object[] args);
        void Error(Exception exception, string message, params object[] args);

        void Fatal(string message, params object[] args);
        void Fatal(Exception exception, string message, params object[] args);
    }
}