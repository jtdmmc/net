using DotnetSoftwarePlatform.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.Common.Helpers
{
    public static class LoggerExtensions
    {
        public static void Information(this ILogger logger, object message)
        {
            logger.Log(new LogEntry(LoggingEventType.Information, message.ToString()));
        }

        public static void Information(this ILogger logger, object message, Exception exception)
        {
            logger.Log(new LogEntry(LoggingEventType.Information, message.ToString(), exception));
        }

        public static void InformationFormat(this ILogger logger, string format, params object[] args)
        {
            logger.Log(new LogEntry(LoggingEventType.Information, String.Format(format, args)));
        }

        public static void Debug(this ILogger logger, object message)
        {
            logger.Log(new LogEntry(LoggingEventType.Debug, message.ToString()));
        }

        public static void Debug(this ILogger logger, object message, Exception exception)
        {
            logger.Log(new LogEntry(LoggingEventType.Debug, message.ToString(), exception));
        }

        public static void DebugFormat(this ILogger logger, string format, params object[] args)
        {
            logger.Log(new LogEntry(LoggingEventType.Debug, String.Format(format, args)));
        }

        public static void Warn(this ILogger logger, object message)
        {
            logger.Log(new LogEntry(LoggingEventType.Warning, message.ToString()));
        }

        public static void Warn(this ILogger logger, object message, Exception exception)
        {
            logger.Log(new LogEntry(LoggingEventType.Warning, message.ToString(), exception));
        }

        public static void WarnFormat(this ILogger logger, string format, params object[] args)
        {
            logger.Log(new LogEntry(LoggingEventType.Warning, String.Format(format, args)));
        }

        public static void Error(this ILogger logger, object message)
        {
            logger.Log(new LogEntry(LoggingEventType.Error, message.ToString()));
        }

        public static void Error(this ILogger logger, object message, Exception exception)
        {
            logger.Log(new LogEntry(LoggingEventType.Error, message.ToString(), exception));
        }

        public static void ErrorFormat(this ILogger logger, string format, params object[] args)
        {
            logger.Log(new LogEntry(LoggingEventType.Error, String.Format(format, args)));
        }

        public static void Fatal(this ILogger logger, object message)
        {
            logger.Log(new LogEntry(LoggingEventType.Fatal, message.ToString()));
        }

        public static void Fatal(this ILogger logger, object message, Exception exception)
        {
            logger.Log(new LogEntry(LoggingEventType.Fatal, message.ToString(), exception));
        }

        public static void FatalFormat(this ILogger logger, string format, params object[] args)
        {
            logger.Log(new LogEntry(LoggingEventType.Fatal, String.Format(format, args)));
        }
    }
}
