using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Core.Dal.Abstraction.Interfaces.Repositories;

namespace DataAccess.Core.Dal.Implementation.Repositories
{
    public class GenericEntityRepository<TEntity> :
        IGenericEntityRepository<TEntity>
        where TEntity : class
    {
        #region Protected Fields

        protected readonly DbSet<TEntity> DbSet;
        protected readonly DbContext Context;
        private IQueryable<TEntity> _query;

        #endregion

        #region Constructor

        public GenericEntityRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        #endregion Constructor

        #region Query

        public virtual IQueryable<TEntity> Query => DbSet;

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate != null ? Query.Where(predicate) : Query;
        }

        #endregion Query

        #region Get

        #region Async

        #endregion Async

        #region Default

        #endregion Default

        #endregion

        #region Create

        #region Async

        #region Protected Method

        protected virtual async Task<TEntity> BeforeCreateAsync(TEntity entity)
        {
            return entity;
        }

        protected virtual async Task<TEntity> AfterCreateAsync(TEntity entity)
        {
            return entity;
        }

        protected virtual async Task<TEntity> DataBaseCreateAsync(TEntity entity)
        {
            entity = DbSet.Add(entity);

            await SaveAsync();

            return entity;
        }

        #endregion Protected Method

        #region Public

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            entity = await BeforeCreateAsync(entity);

            entity = await DataBaseCreateAsync(entity);

            entity = await AfterCreateAsync(entity);

            return entity;
        }

        #endregion  Public

        #endregion Async

        #region Default

        #region Protected

        protected virtual TEntity BeforeCreate(TEntity entity)
        {
            return entity;
        }

        protected virtual TEntity AfterCreate(TEntity entity)
        {
            return entity;
        }

        protected virtual TEntity DataBaseCreate(TEntity entity)
        {
            entity = DbSet.Add(entity);

            Save();

            return entity;
        }

        #endregion Protected

        #region Public

        public virtual TEntity Create(TEntity entity)
        {
            entity = BeforeCreate(entity);

            entity = DataBaseCreate(entity);

            entity = AfterCreate(entity);

            return entity;
        }

        #endregion Public

        #endregion Default

        #endregion Create

        #region Update

        #region Async

        #region Protected

        protected virtual TEntity BeforeUpdate(TEntity entity)
        {
            return entity;
        }

        protected virtual TEntity AfterUpdate(TEntity entity)
        {
            return entity;
        }

        protected virtual TEntity DataBaseUpdate(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            Context.Entry(entity).State = EntityState.Modified;

            Save();

            return entity;
        }

        #endregion Protected

        #region Public

        public virtual TEntity Update(TEntity entity)
        {
            entity = BeforeUpdate(entity);

            entity = DataBaseUpdate(entity);

            entity = AfterUpdate(entity);

            return entity;
        }

        #endregion Public

        #endregion Async

        #region Default

        #region Protected

        protected virtual async Task<TEntity> BeforeUpdateAsync(TEntity entity)
        {
            return entity;
        }

        protected virtual async Task<TEntity> AfterUpdateAsync(TEntity entity)
        {
            return entity;
        }

        protected virtual async Task<TEntity> DataBaseUpdateAsync(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            Context.Entry(entity).State = EntityState.Modified;

            await SaveAsync();

            return entity;
        }

        #endregion Protected

        #region Public

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity = await BeforeUpdateAsync(entity);

            entity = await DataBaseUpdateAsync(entity);

            entity = await AfterUpdateAsync(entity);

            return entity;
        }

        #endregion Public

        #endregion Default

        #endregion Update

        #region Delete

        #region Async

        #region Protected

        protected virtual async Task<TEntity> BeforeDeleteAsync(TEntity entity)
        {
            return entity;
        }

        protected virtual async Task<TEntity> AfterDeleteAsync(TEntity entity)
        {
            return entity;
        }

        protected virtual async Task<bool> DataBaseDeleteAsync(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            DbSet.Remove(entity);

            return await SaveAsync() > 0;
        }

        #endregion Protected

        #region Public

        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            entity = await BeforeDeleteAsync(entity);

            var resultDeleting = await DataBaseDeleteAsync(entity);

            await AfterDeleteAsync(entity);

            return resultDeleting;
        }

        #endregion Public

        #endregion Async

        #region Default

        #region Protected

        protected virtual TEntity BeforeDelete(TEntity entity)
        {
            return entity;
        }

        protected virtual TEntity AfterDelete(TEntity entity)
        {
            return entity;
        }

        protected virtual bool DataBaseDelete(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            DbSet.Remove(entity);

            return Save() > 0;
        }

        #endregion Protected

        #region Public

        public virtual bool Delete(TEntity entity)
        {
            entity = BeforeDelete(entity);

            var resultDeleting = DataBaseDelete(entity);

            AfterDelete(entity);

            return resultDeleting;
        }

        #endregion Public

        #endregion Default

        #endregion Delete

        #region Save

        #region Async

        protected async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }

        #endregion Async

        #region Default

        protected int Save()
        {
            return Context.SaveChanges();
        }

        #endregion Default

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Context.Dispose();
        }

        #endregion
    }
}
