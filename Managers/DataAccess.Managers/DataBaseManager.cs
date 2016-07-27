using DataAccess.Dto.Dal.Abstraction.Interfaces.Interfaces;
using DataAccess.Managers.Abstraction;

namespace DataAccess.Managers
{
    public class DataBaseManager : IDataBaseManager
    {

        #region Constructors

        public DataBaseManager(
            IUserRepositoryResolver userResolver,
            IRoleRepositoryResolver roleResolver
            )
        {
            UserRepositoryResolver = userResolver;
            RoleRepositoryResolver = roleResolver;
        }

        #endregion

        #region IDataBaseManager
        public IUserRepositoryResolver UserRepositoryResolver { get; }
        public IRoleRepositoryResolver RoleRepositoryResolver { get; }
        #endregion
    }
}
