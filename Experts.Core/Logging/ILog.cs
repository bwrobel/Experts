using System;

namespace Experts.Core.Logging
{
    public interface ILog
    {
        ILog SetSource(Type source);
        ILogEntry Debug(string message, params object[] args);
        ILogEntry Info(string message, params object[] args);
        ILogEntry Warn(string message, params object[] args);
        ILogEntry Error(string message, params object[] args);
        ILogEntry Fatal(string message, params object[] args);
    }
}