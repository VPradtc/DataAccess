using System;
using Ninject;
using DataAccess.Core.Dal.Abstraction.Interfaces.Repositories;
using DataAccess.Core.Model.Abstraction.Interfaces;
using DataAccess.CoreDto.Dal.Abstraction.Interfaces.Repositories;

namespace DataAccess.CoreDto.Dal.Implementation.Repositories
{
    public class TrackableDtoRepositoryResolver<TKey, TEntity, TTrackableKey, TEntityRepository> :
        TimeTrackableDtoRepositoryResolver<TKey,TEntity, TEntityRepository>,
        ITrackableDtoRepositoryResolver<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntityRepository : ITrackableEntityRepository<TKey, TEntity, TTrackableKey>
        where TEntity : class, IPrimary<TKey>,IStatable,ITrackable<TTrackableKey>
    {
        #region Constructor

        public TrackableDtoRepositoryResolver(TEntityRepository repository, IKernel serviceLocator) : base(repository, serviceLocator)
        {
        }

        #endregion Constructor

    }
}
