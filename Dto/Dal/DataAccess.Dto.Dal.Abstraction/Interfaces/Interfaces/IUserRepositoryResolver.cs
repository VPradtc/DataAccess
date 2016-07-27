using System;
using DataAccess.CoreDto.Dal.Abstraction.Interfaces.Repositories;
using DataAccess.Entities.Identity;
using System.Threading.Tasks;
using DataAccess.Entities.Enums;
using DataAccess.Dto.Model.Validation;

namespace DataAccess.Dto.Dal.Abstraction.Interfaces.Interfaces
{
    public interface IUserRepositoryResolver : IStatableDtoRepositoryResolve<Guid, User>
    {
        Task<TDto> CreateAsync<TDto>(TDto dto, string password, RoleIdentifier roleIdentifier);
        Task<TDto> UpdateAsync<TDto>(TDto dto, RoleIdentifier roleIdentifier);

        Task<UniqueValidationResult> IsUniqueEmail(string username);
    }
}
