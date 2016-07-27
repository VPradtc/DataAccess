using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using DataAccess.Core.Dal.Abstraction.Interfaces.Repositories;
using DataAccess.Core.Model.Abstraction.Interfaces;

namespace DataAccess.Core.Dal.Implementation.Repositories
{
    public class TrackableEntityRepository<TKey, TEntity, TTrackableKey, TPermissionsKey> :
        TimeTrackableEntityRepository<TKey, TEntity, TPermissionsKey>,
        ITrackableEntityRepository<TKey, TEntity, TTrackableKey>
        where TEntity : class, IPrimary<TKey>, IStatable, ITrackable<TTrackableKey>
        where TKey : IEquatable<TKey>
    {
        private readonly Lazy<TTrackableKey> _currentUserId;

        public TTrackableKey CurrentUserId => _currentUserId.Value;

        #region Constructor

        public TrackableEntityRepository(DbContext context)
            : base(context)
        {
            _currentUserId = new Lazy<TTrackableKey>(GetCurrentUserId);
        }

        #endregion

        #region Overriden Public Method

        #endregion

        #region Overriden Protected Method

        #region Async

        protected override async Task<TEntity> BeforeCreateAsync(TEntity entity)
        {
            entity.CreatedBy = CurrentUserId;
            entity.ModifiedBy = CurrentUserId;

            return await base.BeforeCreateAsync(entity);
        }

        protected override async Task<TEntity> BeforeUpdateAsync(TEntity entity)
        {
            entity.ModifiedBy = CurrentUserId;

            return await base.BeforeUpdateAsync(entity);
        }

        protected override async Task<TEntity> BeforeActivateAsync(TEntity entity)
        {
            entity.ModifiedBy = CurrentUserId;

            return await base.BeforeActivateAsync(entity);
        }

        protected override async Task<TEntity> BeforeDeactivateAsync(TEntity entity)
        {
            entity.ModifiedBy = CurrentUserId;

            return await base.BeforeDeactivateAsync(entity);
        }

        #endregion

        #region Default

        protected override TEntity BeforeCreate(TEntity entity)
        {
            entity.CreatedBy = CurrentUserId;
            entity.ModifiedBy = CurrentUserId;

            return base.BeforeCreate(entity);
        }

        protected override TEntity BeforeUpdate(TEntity entity)
        {
            entity.ModifiedBy = CurrentUserId;

            return base.BeforeUpdate(entity);
        }

        protected override TEntity BeforeActivate(TEntity entity)
        {
            entity.ModifiedBy = CurrentUserId;

            return base.BeforeActivate(entity);
        }

        protected override TEntity BeforeDeactivate(TEntity entity)
        {
            entity.ModifiedBy = CurrentUserId;

            return base.BeforeDeactivate(entity);
        }

        #endregion Default

        #endregion

        #region Additional Methods

        protected virtual TTrackableKey GetCurrentUserId()
        {
            string userId = Thread.CurrentPrincipal.Identity.GetUserId();

            if (String.IsNullOrWhiteSpace(userId))
            {
                return default(TTrackableKey);
            }

            return Convert(userId);
        }

        protected TTrackableKey Convert(string input)
        {
            var converter = TypeDescriptor.GetConverter(typeof(TTrackableKey));
            return (TTrackableKey)converter.ConvertFromString(input);
        }

        #endregion Additional Methods
    }
}
