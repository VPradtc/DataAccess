namespace DataAccess.CoreDto.Dal.Abstraction.Interfaces.Repositories
{
    public interface ITrackableDtoRepositoryResolver<in TKey, TEntity> :
        ITimeTrackableDtoRepositoryResolver<TKey, TEntity>
        where TEntity : class
    {

    }
}