using DataAccess.Dto.Dal.Abstraction.Interfaces.Interfaces;

namespace DataAccess.Managers.Abstraction
{
    public interface IDataBaseManager
    {
        #region Properties
        IUserRepositoryResolver UserRepositoryResolver { get; }
        IRoleRepositoryResolver RoleRepositoryResolver { get; }
        #endregion
    }
}