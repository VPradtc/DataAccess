using System;
using DataAccess.Core.Dal.Abstraction.Interfaces.Repositories;
using DataAccess.Entities.Identity;
using DataAccess.Entities.Enums;

namespace DataAccess.Dal.Abstraction.Interfaces
{
    public interface IRoleRepository : IPrimaryEntityRepository<Guid, Role>
    {
        Role GetByName(string name);
        Role GetByIdentifier(RoleIdentifier identifier);
    }
}
