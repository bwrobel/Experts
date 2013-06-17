using System;
using Experts.Core.Logging.Log4NetLog;

namespace Experts.Web.Logging
{
    public class WebLogFactory
    {
        public static void Initialize()
        {
            Log4NetLogFactory.Initialize();
        }

        public static WebLog CreateNew()
        {
            return new WebLog(Environment.CurrentDirectory);
        }
    }
}