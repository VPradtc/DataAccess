using System;
using log4net;
using log4net.Config;
using DataAccess.ApplicationLogger.Abstraction.Interfaces;

namespace DataAccess.ApplicationLogger
{
    public class Logger : IApplicationLogger
    {
        #region private Members

        private readonly ILog _logger;

        #endregion

        #region Constructors

        public Logger(Type targetType)
        {
            _logger = LogManager.GetLogger(targetType);
        }

        public static void Configure()
        {
            XmlConfigurator.Configure();
        }

        #endregion

        #region Error IErrorLogger

        public void LogError(object message)
        {
            _logger.Error(message);
        }

        public void LogError(Exception exception)
        {
            _logger.Error(exception.Message, exception);
        }

        public void LogError(object message, Exception exception)
        {
            _logger.Error(message, exception);
        }

        #endregion

        #region Debug IDebugLogger

        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        #endregion

        #region Info IInfoLogger

        public void LogInfo(object message)
        {
            _logger.Info(message);
        }

        #endregion

    }
}
