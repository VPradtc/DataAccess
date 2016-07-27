using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Core.Dal.Abstraction.Interfaces.Repositories;
using DataAccess.Core.Model.Abstraction.Interfaces;

namespace DataAccess.Core.Dal.Implementation.Repositories
{
    public class PrimaryEntityRepository<TKey, TEntity, TPermissionsKey> :
        PermissionsRepository<TEntity, TPermissionsKey>,
        IPrimaryEntityRepository<TKey, TEntity>
        where TEntity : class, IPrimary<TKey>
        where TKey : IEquatable<TKey>
    {

        #region Constructor

        public PrimaryEntityRepository(DbContext context) : base(context)
        {
        }

        #endregion Constructor

        #region Get By Id

        #region Async

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await Query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async virtual Task<bool> DeleteAsync(TKey id)
        {
            var entity = await GetByIdAsync(id);

            return await DeleteAsync(entity);
        }

        #endregion Async

        #region Default

        public virtual TEntity GetById(TKey id)
        {
            return Query.FirstOrDefault(e => e.Id.Equals(id));
        }

        public virtual bool Delete(TKey id)
        {
            var entity = GetById(id);

            return Delete(entity);
        }

        #endregion Default

        #endregion Get By Id
    }
}
