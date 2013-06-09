using System;

namespace Experts.Core.Logging
{
    public interface ILogEntry
    {
        ILogEntry WithException(Exception ex);
        void Proceed();
    }
}