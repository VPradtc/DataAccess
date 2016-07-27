using System;
using DataAccess.CoreDto.Dal.Implementation.Repositories;
using DataAccess.Dto.Dal.Abstraction.Interfaces.Interfaces;
using DataAccess.Entities.Identity;
using DataAccess.Dal.Abstraction.Interfaces;
using DataAccess.Dal.Identity;
using AutoMapper;
using System.Threading.Tasks;
using DataAccess.Dto.Model.Models;
using DataAccess.Entities.Enums;
using System.Linq;
using DataAccess.Model.Definition.Providers.Identity;
using DataAccess.Dto.Model.Validation;
using Ninject;

namespace DataAccess.Dto.Dal.Implementation.Repositories
{
    public class UserRepositoryResolver :
        TrackableDtoRepositoryResolver<Guid, User, Guid?, IUserRepository>,
        IUserRepositoryResolver
    {
        #region Constructors
        public UserRepositoryResolver(IUserRepository repository, IKernel serviceLocator) : base(repository, serviceLocator)
        {
        }

        #endregion Constructors

        public async Task<TDto> CreateAsync<TDto>(TDto dto, string password, RoleIdentifier roleIdentifier)
        {
            var entity = Mapper.Map<User>(dto);

            var result = await EntityRepository.CreateAsync(entity, password, roleIdentifier);

            return Mapper.Map<TDto>(result);
        }

        public async Task<TDto> UpdateAsync<TDto>(TDto dto, RoleIdentifier roleIdentifier)
        {
            var entity = Mapper.Map<User>(dto);

            var dbEntity = await EntityRepository.GetByIdAsync(entity.Id);

            var updatedEntity = Mapper.Map(dto, dbEntity);

            var result = await EntityRepository.UpdateAsync(updatedEntity, roleIdentifier);

            return Mapper.Map<TDto>(result);
        }

        public async Task<UniqueValidationResult> IsUniqueEmail(string email)
        {
            var isUnique = await EntityRepository.IsUniqueEmailAsync(email);

            var result = Mapper.Map<UniqueValidationResult>(isUnique);

            return result;
        }
    }
}
