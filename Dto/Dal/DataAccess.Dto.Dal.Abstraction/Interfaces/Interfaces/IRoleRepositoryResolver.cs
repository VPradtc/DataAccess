using System;
using DataAccess.CoreDto.Dal.Abstraction.Interfaces.Repositories;
using DataAccess.Entities.Identity;
using DataAccess.Entities.Enums;

namespace DataAccess.Dto.Dal.Abstraction.Interfaces.Interfaces
{
    public interface IRoleRepositoryResolver : IPrimaryDtoRepositoryResolver<Guid, Role>
    {
        TDto GetByName<TDto>(string name);
        TDto GetByIdentifier<TDto>(RoleIdentifier identifier);
    }
}
