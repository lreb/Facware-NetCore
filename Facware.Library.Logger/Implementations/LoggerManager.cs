using System;
using Facware.Library.Logger.Interfaces;
using NLog;

namespace Facware.Library.Logger.Implementations
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public LoggerManager()
        {
        }

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogDebug(Exception ex, string message)
        {
            logger.Debug(ex, message);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }

        public void LogError(Exception ex, string message)
        {
            logger.Error(ex, message);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogInfo(Exception ex, string message)
        {
            logger.Info(ex, message);
        }

        public void LogWarn(string message)
        {
            logger.Warn(message);
        }

        public void LogWarn(Exception ex, string message)
        {
            logger.Warn(ex, message);
        }
    }
}
