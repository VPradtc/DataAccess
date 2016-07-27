namespace DataAccess.ApplicationLogger.Abstraction.Interfaces
{
    public interface IInfoLogger
    {
        #region Methods

        void LogInfo(object message);

        #endregion
    }
}
