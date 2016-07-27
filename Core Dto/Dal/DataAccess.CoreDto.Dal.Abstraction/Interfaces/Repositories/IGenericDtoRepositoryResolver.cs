using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.CoreDto.Dal.Abstraction.Interfaces.Repositories
{
    public interface IGenericDtoRepositoryResolver<TEntity> :
        IDisposable
        where TEntity : class
    {
        #region Query

        IQueryable<TDto> Get<TDto>(Expression<Func<TEntity, bool>> predicate = null);

        #endregion Query

        #region Async

        Task<TDto> CreateAsync<TDto>(TDto dto);
        Task<TDto> UpdateAsync<TDto>(TDto dto);
        Task<TResultDto> CreateAsync<TDto,TResultDto>(TDto dto);
        Task<TResultDto> UpdateAsync<TDto, TResultDto>(TDto dto);
        Task<bool> DeleteAsync<TDto>(TDto dto);

        #endregion Async

        #region Default

        TDto Create<TDto>(TDto dto);
        TDto Update<TDto>(TDto dto);
        TResultDto Create<TDto, TResultDto>(TDto dto);
        TResultDto Update<TDto, TResultDto>(TDto dto);
        bool Delete<TDto>(TDto dto);

        #endregion Default

    }
}