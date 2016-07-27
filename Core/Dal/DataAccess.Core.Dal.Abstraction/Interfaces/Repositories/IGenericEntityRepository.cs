using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Core.Dal.Abstraction.Interfaces.Repositories
{
    public interface IGenericEntityRepository<TEntity> : IDisposable
        where TEntity : class
    {

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null);

        #region Async

        Task<TEntity> CreateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);

        #endregion Async

        #region Default

        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        bool Delete(TEntity entity);

        #endregion Default
    }
}
