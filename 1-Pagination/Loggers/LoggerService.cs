using NLog;
namespace _1_Pagination.Loggers
{
    public class LoggerService : ILoggerService
    {
        private static NLog.ILogger logger = NLog.LogManager.GetCurrentClassLogger();
        public void LogDebug(string message) => logger.Debug(message);

        public void LogError(string message) => logger.Error(message);

        public void LogInfo(string message) => logger.Info(message);

        public void LogWarning(string message) => logger.Warn(message);
    }
}
