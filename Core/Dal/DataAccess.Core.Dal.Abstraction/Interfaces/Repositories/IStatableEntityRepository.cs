using System;
using System.Threading.Tasks;
using DataAccess.Core.Model.Abstraction.Interfaces;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Core.Dal.Abstraction.Interfaces.Repositories
{
    public interface IStatableEntityRepository<in TKey, TEntity> :
        IPrimaryEntityRepository<TKey, TEntity>
        where TEntity : class, IPrimary<TKey> ,IStatable
        where TKey : IEquatable<TKey>
    {
        IQueryable<TEntity> GetActive(Expression<Func<TEntity, bool>> predicate = null);
        IQueryable<TEntity> GetInactive(Expression<Func<TEntity, bool>> predicate = null);

        Task<TEntity> ActivateAsync(TEntity entity);
        Task<TEntity> DeactivateAsync(TEntity entity);


        TEntity Deactivate(TEntity entity);
        TEntity Activate(TEntity entity);
    }
}
