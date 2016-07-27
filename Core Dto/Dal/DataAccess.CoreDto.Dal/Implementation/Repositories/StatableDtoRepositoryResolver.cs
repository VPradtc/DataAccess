using System;
using System.Threading.Tasks;
using Ninject;
using DataAccess.Core.Dal.Abstraction.Interfaces.Repositories;
using DataAccess.Core.Model.Abstraction.Interfaces;
using DataAccess.CoreDto.Dal.Abstraction.Interfaces.Repositories;

namespace DataAccess.CoreDto.Dal.Implementation.Repositories
{
    public class StatableDtoRepositoryResolver<TKey, TEntity, TEntityRepository> :
        PrimaryDtoRepositoryResolver<TKey, TEntity, TEntityRepository>,
        IStatableDtoRepositoryResolve<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntityRepository : IStatableEntityRepository<TKey, TEntity>
        where TEntity : class, IPrimary<TKey>, IStatable
    {
        #region Constructor

        public StatableDtoRepositoryResolver(TEntityRepository repository, IKernel serviceLocator) : base(repository, serviceLocator)
        {
        }

        #endregion Constructor

        #region Deactivate

        public async virtual Task<TEntity> DeactivateAsync(TKey id)
        {
            var primaryEntity = await EntityRepository.GetByIdAsync(id);

            return await EntityRepository.DeactivateAsync(primaryEntity);
        }

        public virtual TEntity Deactivate(TKey id)
        {
            var primaryEntity = EntityRepository.GetById(id);

            return EntityRepository.Deactivate(primaryEntity);
        }

        #endregion Deactivate

        #region Activate

        public async virtual Task<TEntity> ActivateAsync(TKey id)
        {
            var primaryEntity = await EntityRepository.GetByIdAsync(id);

            return await EntityRepository.ActivateAsync(primaryEntity);
        }

        public virtual TEntity Activate(TKey id)
        {
            var primaryEntity = EntityRepository.GetById(id);

            return EntityRepository.Activate(primaryEntity);
        }

        #endregion Deactivate
    }
}
