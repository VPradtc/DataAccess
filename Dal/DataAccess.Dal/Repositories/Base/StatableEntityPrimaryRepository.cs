using System;
using System.Data.Entity;
using DataAccess.Core.Dal.Implementation.Repositories;
using DataAccess.Core.Model.Abstraction.Interfaces;
using DataAccess.Entities.Enums;

namespace DataAccess.Dal.Repositories.Base
{
    public class StatableEntityPrimaryRepository<TEntity> :
        StatableEntityRepository<Guid, TEntity, RoleIdentifier>
       where TEntity : class, IPrimary<Guid>, IStatable
    {
        public StatableEntityPrimaryRepository(DbContext context) : base(context)
        {
        }

        protected override RoleIdentifier CurentPermission => RoleRepository.GetLoggedUserMaxRole();
    }
}
