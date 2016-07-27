using System;
using DataAccess.Core.Model.Abstraction.Interfaces;

namespace DataAccess.Core.Dal.Abstraction.Interfaces.Repositories
{
    public interface ITimeTrackableEntityRepository<in TKey, TEntity> :
        IStatableEntityRepository<TKey, TEntity>
        where TEntity : class, IPrimary<TKey>, IStatable, ITimeTrackable
        where TKey : IEquatable<TKey>
    {

    }
}
