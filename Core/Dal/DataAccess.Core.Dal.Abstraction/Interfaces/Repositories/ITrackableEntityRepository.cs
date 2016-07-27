using System;
using DataAccess.Core.Model.Abstraction.Interfaces;

namespace DataAccess.Core.Dal.Abstraction.Interfaces.Repositories
{
    public interface ITrackableEntityRepository<in TKey, TEntity, TTrackableKey> :
        ITimeTrackableEntityRepository<TKey, TEntity>
        where TEntity : class, IPrimary<TKey>, IStatable, ITrackable<TTrackableKey>
        where TKey : IEquatable<TKey>
    {

    }
}
