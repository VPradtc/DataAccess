using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccess.Core.Dal.Abstraction.Interfaces.Repositories;
using DataAccess.Core.Model.Abstraction.Interfaces;
using DataAccess.CoreDto.Dal.Abstraction.Interfaces.Repositories;
using DataAccess.CoreDto.Model.Kendo;
using System.Collections.Generic;
using Ninject;
using DataAccess.CoreDto.Model.Kendo.Filtering;
using DataAccess.CoreDto.Model.Kendo.Sorting.Core.Service;

namespace DataAccess.CoreDto.Dal.Implementation.Repositories
{
    public class PrimaryDtoRepositoryResolver<TKey, TEntity, TEntityRepository> :
        GenericDtoRepositoryResolver<TEntity, TEntityRepository>,
        IPrimaryDtoRepositoryResolver<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntityRepository : IPrimaryEntityRepository<TKey, TEntity>
        where TEntity : class, IPrimary<TKey>
    {
        protected readonly IKernel _serviceLocator;
        #region Constructor

        public PrimaryDtoRepositoryResolver(TEntityRepository repository,
            IKernel serviceLocator) : base(repository)
        {
            _serviceLocator = serviceLocator;
        }

        #endregion Constructor

        #region Async

        public async virtual Task<TDto> GetByIdAsync<TDto>(TKey id)
        {
            var entity = await EntityRepository.GetByIdAsync(id);

            return Mapper.Map<TDto>(entity);
        }

        public override async Task<bool> DeleteAsync<TDto>(TDto dto)
        {
            TEntity primaryEntity = null;
            if (dto is IPrimary<TKey>)
            {
                var id = (dto as IPrimary<TKey>).Id;
                primaryEntity = await EntityRepository.GetByIdAsync(id);
            }

            var entity = Mapper.Map(dto, primaryEntity);

            return await EntityRepository.DeleteAsync(entity);
        }

        public sealed override async Task<TResultDto> UpdateAsync<TDto, TResultDto>(TDto dto)
        {
            TEntity primaryEntity = null;
            if (dto is IPrimary<TKey>)
            {
                var id = (dto as IPrimary<TKey>).Id;
                primaryEntity = await EntityRepository.GetByIdAsync(id);
            }

            var entity = MapUpdatedEntity(dto, primaryEntity);

            var updatedEntity = await EntityRepository.UpdateAsync(entity);

            return Mapper.Map<TResultDto>(updatedEntity);
        }

        public virtual TEntity MapUpdatedEntity<TDto>(TDto dto, TEntity dbEntity)
        {
            var entity = Mapper.Map(dto, dbEntity);

            return entity;
        }

        #endregion Async

        #region Default

        public virtual TDto GetById<TDto>(TKey id)
        {
            var entity = EntityRepository.GetById(id);

            return Mapper.Map<TDto>(entity);
        }

        public override TResultDto Update<TDto, TResultDto>(TDto dto)
        {
            TEntity primaryEntity = null;
            if (dto is IPrimary<TKey>)
            {
                var id = (dto as IPrimary<TKey>).Id;
                primaryEntity = EntityRepository.GetById(id);
            }

            var entity = Mapper.Map(dto, primaryEntity);

            var updatedEntity = EntityRepository.Update(entity);

            return Mapper.Map<TResultDto>(updatedEntity);
        }

        public override bool Delete<TDto>(TDto dto)
        {
            TEntity primaryEntity = null;
            if (dto is IPrimary<TKey>)
            {
                var id = (dto as IPrimary<TKey>).Id;
                primaryEntity = EntityRepository.GetById(id);
            }

            var entity = Mapper.Map(dto, primaryEntity);

            return EntityRepository.Delete(entity);
        }
        #endregion Default

        #region GetByPage

        #region Async

        public async Task<KendoGridResponse<TDto>> GetByPageAsync<TDto>(
            KendoGridRequest request,
            string defaultSortExpression,
            Expression<Func<TEntity, bool>> filter,
            IDictionary<string, object> projectionParameters)
        {
            var query = EntityRepository.Get(filter);

            var dtoQuery = query.ProjectTo<TDto>(projectionParameters);

            dtoQuery = ApplyFiltering(request, dtoQuery);

            dtoQuery = ApplySorting(request, dtoQuery, defaultSortExpression);

            return
                await KendoGridResponse<TDto>.
                    GenerateResponseAsync(dtoQuery, request.Page, request.PageSize);
        }

        protected IQueryable<TDto> ApplyFiltering<TDto>(KendoGridRequest request, IQueryable<TDto> collection)
        {
            var filterCount = request.Filter?.Filters?.Count;

            if (filterCount == null
                || filterCount == 0)
            {
                return collection;
            }

            var filterExpressionBuilder = (FilterExpressionBuilder<TDto>)_serviceLocator.GetService(typeof(IFilterExpressionBuilder<TDto>));

            return filterExpressionBuilder.CreatedFilteredCollection(collection, request.Filter);
        }

        protected IQueryable<TDto> ApplySorting<TDto>(KendoGridRequest request, IQueryable<TDto> collection, string defaultSortExpression)
        {
            if (request.Sort == null || request.Sort.Count <= 0)
            {
                return collection.OrderBy(defaultSortExpression);
            }

            var sortableExpressionMapper = (IDynamicSortingService<TDto>)_serviceLocator.GetService(typeof(IDynamicSortingService<TDto>));

            var result = request.Sort.Aggregate(collection,
                (current, sortItem) =>
                {
                    return sortableExpressionMapper.OrderBy(collection, sortItem.Field, sortItem.Dir);
                });

            return result;
        }

        public async Task<KendoGridResponse<TDto>> GetByPageAsync<TDto>(
            KendoGridRequest request,
            string defaultSortExpression,
            Expression<Func<TEntity, bool>> filter)
        {
            return await GetByPageAsync<TDto>(request, defaultSortExpression, filter, null);
        }

        public async Task<KendoGridResponse<TDto>> GetByPageAsync<TDto>(KendoGridRequest request,
            string defaultSortExpression)
        {
            return await GetByPageAsync<TDto>(request, defaultSortExpression, null);
        }

        #endregion Async

        #region Default

        public KendoGridResponse<TDto> GetByPage<TDto>(KendoGridRequest request, string defaultSortExpression, Expression<Func<TEntity, bool>> filter, IDictionary<string, object> projectionParameters)
        {
            var task = GetByPageAsync<TDto>(request, defaultSortExpression, filter, projectionParameters);
            task.Wait();
            return task.Result;
        }

        public KendoGridResponse<TDto> GetByPage<TDto>(KendoGridRequest request, string defaultSortExpression, Expression<Func<TEntity, bool>> filter)
        {
            return GetByPage<TDto>(request, defaultSortExpression, filter, null);
        }

        public KendoGridResponse<TDto> GetByPage<TDto>(KendoGridRequest request, string defaultSortExpression)
        {
            return GetByPage<TDto>(request, defaultSortExpression, null);
        }

        public async Task<bool> DeleteAsync(TKey id)
        {
            return await EntityRepository.DeleteAsync(id);
        }

        public bool Delete(TKey id)
        {
            return EntityRepository.Delete(id);
        }

        #endregion Default

        #endregion GetByPage
    }
}
