using DotnetSoftwarePlatform.Common.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.AutofacLogging
{
    public sealed class Log4NetAdapter : ILogger
    {
        private readonly ILog _log4net;
        public Log4NetAdapter(ILog log)
        {
            _log4net = log;
        }

        public void Log(LogEntry entry)
        {
            switch (entry.Severity)
            {
                case LoggingEventType.Debug:
                    _log4net.Debug(entry.Message, entry.Exception);
                    break;
                case LoggingEventType.Error:
                    _log4net.Error(entry.Message, entry.Exception);
                    break;
                case LoggingEventType.Fatal:
                    _log4net.Fatal(entry.Message, entry.Exception);
                    break;
                case LoggingEventType.Information:
                    _log4net.Info(entry.Message, entry.Exception);
                    break;
                case LoggingEventType.Warning:
                    _log4net.Warn(entry.Message, entry.Exception);
                    break;
                default:
                    _log4net.Info(entry.Message, entry.Exception);
                    break;
            }
        }
    }
}
