using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.CoreDto.Model.Kendo;
using System.Collections.Generic;

namespace DataAccess.CoreDto.Dal.Abstraction.Interfaces.Repositories
{
    public interface IPrimaryDtoRepositoryResolver<in TKey, TEntity> :
        IGenericDtoRepositoryResolver<TEntity>
        where TEntity : class
    {
        #region Async

        Task<KendoGridResponse<TDto>> GetByPageAsync<TDto>(KendoGridRequest request, string defaultSortExpression);
        Task<KendoGridResponse<TDto>> GetByPageAsync<TDto>(KendoGridRequest request, string defaultSortExpression, Expression<Func<TEntity, bool>> filter);
        Task<KendoGridResponse<TDto>> GetByPageAsync<TDto>(KendoGridRequest request, string defaultSortExpression, Expression<Func<TEntity, bool>> filter, IDictionary<string, object> projectionParameters);
        Task<TDto> GetByIdAsync<TDto>(TKey id);

        Task<bool> DeleteAsync(TKey id);

        #endregion Async

        #region Default

        KendoGridResponse<TDto> GetByPage<TDto>(KendoGridRequest request, string defaultSortExpression);
        KendoGridResponse<TDto> GetByPage<TDto>(KendoGridRequest request, string defaultSortExpression, Expression<Func<TEntity, bool>> filter);
        KendoGridResponse<TDto> GetByPage<TDto>(KendoGridRequest request, string defaultSortExpression, Expression<Func<TEntity, bool>> filter, IDictionary<string, object> projectionParameters);
        TDto GetById<TDto>(TKey id);

        bool Delete(TKey id);

        #endregion Default
    }
}