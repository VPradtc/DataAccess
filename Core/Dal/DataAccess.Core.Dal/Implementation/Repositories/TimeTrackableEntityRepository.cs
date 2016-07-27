using System;
using System.Data.Entity;
using System.Threading.Tasks;
using DataAccess.Core.Dal.Abstraction.Interfaces.Repositories;
using DataAccess.Core.Model.Abstraction.Interfaces;

namespace DataAccess.Core.Dal.Implementation.Repositories
{
    public class TimeTrackableEntityRepository<TKey, TEntity, TPermissionsKey> :
        StatableEntityRepository<TKey, TEntity, TPermissionsKey>,
        ITimeTrackableEntityRepository<TKey, TEntity>
        where TEntity : class, IPrimary<TKey>, IStatable, ITimeTrackable
        where TKey : IEquatable<TKey>
    {
        public TimeTrackableEntityRepository(DbContext context)
            : base(context)
        {
        }

        protected override async Task<TEntity> BeforeCreateAsync(TEntity entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.ModifiedDate = DateTime.UtcNow;

            return await base.BeforeCreateAsync(entity);
        }

        protected override async Task<TEntity> BeforeUpdateAsync(TEntity entity)
        {
            entity.ModifiedDate = DateTime.UtcNow;

            return await base.BeforeUpdateAsync(entity);
        }

        protected override async Task<TEntity> BeforeActivateAsync(TEntity entity)
        {
            entity.ModifiedDate = DateTime.UtcNow;

            return await base.BeforeActivateAsync(entity);
        }

        protected override async Task<TEntity> BeforeDeactivateAsync(TEntity entity)
        {
            entity.ModifiedDate = DateTime.UtcNow;

            return await base.BeforeDeactivateAsync(entity);
        }

        protected override TEntity BeforeCreate(TEntity entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.ModifiedDate = DateTime.UtcNow;

            return base.BeforeCreate(entity);
        }

        protected override TEntity BeforeUpdate(TEntity entity)
        {
            entity.ModifiedDate = DateTime.UtcNow;

            return base.BeforeUpdate(entity);
        }

        protected override TEntity BeforeActivate(TEntity entity)
        {
            entity.ModifiedDate = DateTime.UtcNow;

            return base.BeforeActivate(entity);
        }

        protected override TEntity BeforeDeactivate(TEntity entity)
        {
            entity.ModifiedDate = DateTime.UtcNow;

            return base.BeforeDeactivate(entity);
        }
    }
}
