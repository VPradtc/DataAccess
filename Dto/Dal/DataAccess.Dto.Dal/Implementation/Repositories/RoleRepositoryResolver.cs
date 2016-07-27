using System;
using AutoMapper;
using DataAccess.CoreDto.Dal.Implementation.Repositories;
using DataAccess.Dto.Dal.Abstraction.Interfaces.Interfaces;
using DataAccess.Dto.Model.Models;
using DataAccess.Entities.Identity;
using DataAccess.Dal.Abstraction.Interfaces;
using DataAccess.Entities.Enums;
using Ninject;

namespace DataAccess.Dto.Dal.Implementation.Repositories
{
    public class RoleRepositoryResolver :
        PrimaryDtoRepositoryResolver<Guid, Role, IRoleRepository>, IRoleRepositoryResolver
    {
        #region Constructors

        public RoleRepositoryResolver(IRoleRepository repository, IKernel serviceLocator) : base(repository, serviceLocator)
        {
        }

        #endregion Constructors

        public TDto GetByName<TDto>(string name)
        {
            var entity = EntityRepository.GetByName(name);

            return Mapper.Map<TDto>(entity);
        }
        public TDto GetByIdentifier<TDto>(RoleIdentifier identifier)
        {
            var entity = EntityRepository.GetByIdentifier(identifier);

            return Mapper.Map<TDto>(entity);
        }
    }
}
