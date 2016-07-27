using System;
using System.Threading.Tasks;
using DataAccess.Core.Model.Abstraction.Interfaces;

namespace DataAccess.Core.Dal.Abstraction.Interfaces.Repositories
{
    public interface IPrimaryEntityRepository<in TKey, TEntity> :
        IGenericEntityRepository<TEntity>
        where TEntity : class, IPrimary<TKey>
        where TKey : IEquatable<TKey>
    {
        #region Async

        Task<TEntity> GetByIdAsync(TKey id);
        Task<bool> DeleteAsync(TKey id);

        #endregion Async

        #region Default

        TEntity GetById(TKey id);
        bool Delete(TKey id);

        #endregion Default
    }
}
