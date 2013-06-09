﻿using System;

namespace Experts.Core.Logging.Log4NetLog
{
    public class Log4NetLogFactory : ILogFactory
    {
        private static bool _initialized;

        public static void Initialize()
        {
            if (_initialized) return;
            
            log4net.Config.XmlConfigurator.Configure();

            _initialized = true;
        }

        public static ILog CreateNew(Type source)
        {
            return new Log4NetLog(Environment.CurrentDirectory)
                .SetSource(source);   
        }

        public ILog New(Type source)
        {
            return CreateNew(source);
        }
    }
}