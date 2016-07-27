using System;
using System.Data.Entity;
using DataAccess.Core.Dal.Implementation.Repositories;
using DataAccess.Core.Model.Abstraction.Interfaces;
using DataAccess.Entities.Enums;

namespace DataAccess.Dal.Repositories.Base
{
    public class TrackableEntityPrimaryRepository<TEntity, TTrackableKey> :
        TrackableEntityRepository<Guid, TEntity, TTrackableKey, RoleIdentifier>
           where TEntity : class, IPrimary<Guid>, IStatable, ITrackable<TTrackableKey>
    {
        public TrackableEntityPrimaryRepository(DbContext context) : base(context)
        {
        }

        protected override RoleIdentifier CurentPermission => RoleRepository.GetLoggedUserMaxRole();
    }
}
