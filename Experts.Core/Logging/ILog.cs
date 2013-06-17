using System;

namespace Experts.Core.Logging
{
    public interface ILog
    {
        void Debug(Type source, string message, params object[] args);
        void Debug(string source, string message, params object[] args);

        void Info(Type source, string message, params object[] args);
        void Info(string source, string message, params object[] args);

        void Warn(Type source, string message, params object[] args);
        void Warn(string source, string message, params object[] args);

        void Warn(Type source, Exception exception, string message, params object[] args);
        void Warn(string source, Exception exception, string message, params object[] args);

        void Error(Type source, string message, params object[] args);
        void Error(string source, string message, params object[] args);

        void Error(Type source, Exception exception, string message, params object[] args);
        void Error(string source, Exception exception, string message, params object[] args);

        void Fatal(Type source, string message, params object[] args);
        void Fatal(string source, string message, params object[] args);

        void Fatal(Type source, Exception exception, string message, params object[] args);
        void Fatal(string source, Exception exception, string message, params object[] args);
    }
}