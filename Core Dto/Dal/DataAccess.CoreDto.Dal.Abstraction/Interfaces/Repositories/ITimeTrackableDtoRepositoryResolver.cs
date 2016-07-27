namespace DataAccess.CoreDto.Dal.Abstraction.Interfaces.Repositories
{
    public interface ITimeTrackableDtoRepositoryResolver<in TKey, TEntity> :
        IStatableDtoRepositoryResolve<TKey, TEntity>
        where TEntity : class
    {

    }
}