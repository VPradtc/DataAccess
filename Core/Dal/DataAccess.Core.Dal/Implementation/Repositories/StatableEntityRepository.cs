using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Core.Dal.Abstraction.Interfaces.Repositories;
using DataAccess.Core.Model.Abstraction.Interfaces;

namespace DataAccess.Core.Dal.Implementation.Repositories
{
    public class StatableEntityRepository<TKey, TEntity, TPermissionsKey> :
        PrimaryEntityRepository<TKey, TEntity, TPermissionsKey>,
        IStatableEntityRepository<TKey, TEntity>
        where TEntity : class, IPrimary<TKey>, IStatable
        where TKey : IEquatable<TKey>
    {

        #region Constructor

        public StatableEntityRepository(DbContext context)
            : base(context)
        {
        }

        #endregion

        #region Overriden Query

        public override IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null)
        {
            return base.Get(predicate).Where(entity => !entity.IsDeleted);
        }

        public IQueryable<TEntity> GetActive(Expression<Func<TEntity, bool>> predicate = null)
        {
            return this.Get(predicate).Where(entity => entity.IsActive);
        }

        public IQueryable<TEntity> GetInactive(Expression<Func<TEntity, bool>> predicate = null)
        {
            return this.Get(predicate).Where(entity => !entity.IsActive);
        }

        #endregion Overriden Query

        #region Overriden Protected Method

        protected override async Task<TEntity> BeforeCreateAsync(TEntity entity)
        {
            entity.IsActive = true;
            entity.IsDeleted = false;

            return await base.BeforeCreateAsync(entity);
        }

        protected override TEntity BeforeCreate(TEntity entity)
        {
            entity.IsActive = true;
            entity.IsDeleted = false;

            return base.BeforeCreate(entity);
        }

        #endregion

        #region Deactivate

        #region Async

        #region Protected

        protected virtual async Task<TEntity> BeforeDeactivateAsync(TEntity entity)
        {
            entity.IsActive = false;
            entity.IsDeleted = true;

            return entity;
        }

        protected virtual async Task<TEntity> AfterDeactivateAsync(TEntity entity)
        {
            return entity;
        }

        protected virtual async Task<TEntity> DataBaseDeactivateAsync(TEntity entity)
        {
            return await UpdateAsync(entity);
        }

        #endregion Protected

        #region Public

        public virtual async Task<TEntity> DeactivateAsync(TEntity entity)
        {
            entity = await BeforeDeactivateAsync(entity);

            entity = await DataBaseDeactivateAsync(entity);

            entity = await AfterDeactivateAsync(entity);

            return entity;
        }

        #endregion Public

        #endregion Async

        #region Default

        #region Protected

        protected virtual TEntity BeforeDeactivate(TEntity entity)
        {
            entity.IsActive = false;
            entity.IsDeleted = true;

            return entity;
        }

        protected virtual TEntity AfterDeactivate(TEntity entity)
        {
            return entity;
        }

        protected virtual TEntity DataBaseDeactivate(TEntity entity)
        {
            Update(entity);

            return entity;
        }

        #endregion Protected

        #region Public

        public virtual TEntity Deactivate(TEntity entity)
        {
            entity = BeforeDeactivate(entity);

            entity = DataBaseDeactivate(entity);

            entity = AfterDeactivate(entity);

            return entity;
        }

        #endregion Public

        #endregion Default

        #endregion Deactivate

        #region Activate

        #region Async

        #region Protected

        protected virtual async Task<TEntity> BeforeActivateAsync(TEntity entity)
        {
            entity.IsActive = true;
            entity.IsDeleted = false;

            return entity;
        }

        protected virtual async Task<TEntity> AfterActivateAsync(TEntity entity)
        {
            return entity;
        }

        protected virtual async Task<TEntity> DataBaseActivateAsync(TEntity entity)
        {
            return await UpdateAsync(entity);
        }

        #endregion Protected

        #region Public

        public virtual async Task<TEntity> ActivateAsync(TEntity entity)
        {
            entity = await BeforeActivateAsync(entity);

            entity = await DataBaseActivateAsync(entity);

            entity = await AfterActivateAsync(entity);

            return entity;
        }

        #endregion Public

        #endregion Async

        #region Default

        #region Protected Method

        protected virtual TEntity BeforeActivate(TEntity entity)
        {
            entity.IsActive = true;
            entity.IsDeleted = false;

            return entity;
        }

        protected virtual TEntity AfterActivate(TEntity entity)
        {
            return entity;
        }

        protected virtual TEntity DataBaseActivate(TEntity entity)
        {
            return Update(entity);
        }

        #endregion Protected Method

        #region Public

        public virtual TEntity Activate(TEntity entity)
        {
            entity = BeforeActivate(entity);

            entity = DataBaseActivate(entity);

            entity = AfterActivate(entity);

            return entity;
        }

        protected override bool DataBaseDelete(TEntity entity)
        {
            var task = Task.Run(() => DataBaseDeleteAsync(entity));
            task.Wait();

            return task.Result;
        }

        protected override async Task<bool> DataBaseDeleteAsync(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            entity.IsDeleted = true;

            return await SaveAsync() > 0;
        }

        #endregion Public

        #endregion Default

        #endregion Activate
    }
}
