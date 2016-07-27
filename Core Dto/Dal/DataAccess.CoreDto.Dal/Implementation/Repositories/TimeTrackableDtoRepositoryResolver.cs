using System;
using Ninject;
using DataAccess.Core.Dal.Abstraction.Interfaces.Repositories;
using DataAccess.Core.Model.Abstraction.Interfaces;
using DataAccess.CoreDto.Dal.Abstraction.Interfaces.Repositories;

namespace DataAccess.CoreDto.Dal.Implementation.Repositories
{
    public class TimeTrackableDtoRepositoryResolver<TKey, TEntity, TEntityRepository> :
        StatableDtoRepositoryResolver<TKey, TEntity, TEntityRepository>,
        ITimeTrackableDtoRepositoryResolver<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntityRepository : ITimeTrackableEntityRepository<TKey, TEntity>
        where TEntity : class, IPrimary<TKey>, IStatable, ITimeTrackable
    {
        public TimeTrackableDtoRepositoryResolver(TEntityRepository repository, IKernel serviceLocator) : base(repository, serviceLocator)
        {
        }
    }
}
