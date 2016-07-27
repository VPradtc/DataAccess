using System;
using System.Data.Entity;
using DataAccess.Core.Dal.Implementation.Repositories;
using DataAccess.Core.Model.Abstraction.Interfaces;
using DataAccess.Entities.Enums;

namespace DataAccess.Dal.Repositories.Base
{
    public class PrimaryEntityPrimaryRepository<TEntity> :
        PrimaryEntityRepository<Guid, TEntity, RoleIdentifier>
        where TEntity : class, IPrimary<Guid>
    {
        public PrimaryEntityPrimaryRepository(DbContext context) : base(context)
        {
        }

        protected override RoleIdentifier CurentPermission => RoleRepository.GetLoggedUserMaxRole();
    }
}
