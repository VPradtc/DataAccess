using System;

namespace DataAccess.ApplicationLogger.Abstraction.Interfaces
{
    public interface IErrorLogger
    {
        #region Methods

        void LogError(Exception exception);
        void LogError(object message);
        void LogError(object message, Exception exception);

        #endregion
    }
}
