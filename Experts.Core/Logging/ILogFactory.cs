using System;

namespace Experts.Core.Logging
{
    public interface ILogFactory
    {
        ILog New(Type source);
    }
}
