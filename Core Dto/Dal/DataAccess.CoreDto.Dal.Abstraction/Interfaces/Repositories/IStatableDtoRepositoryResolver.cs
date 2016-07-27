using System.Threading.Tasks;

namespace DataAccess.CoreDto.Dal.Abstraction.Interfaces.Repositories
{
    public interface IStatableDtoRepositoryResolve<in TKey, TEntity>:IPrimaryDtoRepositoryResolver<TKey,TEntity>
        where TEntity : class
    {
        #region Deactivate

        Task<TEntity> DeactivateAsync(TKey id);

        TEntity Deactivate(TKey id);

        #endregion Deactivate

        #region Activate

        Task<TEntity> ActivateAsync(TKey id);

        TEntity Activate(TKey id);

        #endregion Activate
    }
}