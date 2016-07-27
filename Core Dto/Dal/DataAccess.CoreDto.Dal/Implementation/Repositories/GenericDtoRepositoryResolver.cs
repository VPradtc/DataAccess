using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccess.Core.Dal.Abstraction.Interfaces.Repositories;
using DataAccess.CoreDto.Dal.Abstraction.Interfaces.Repositories;

namespace DataAccess.CoreDto.Dal.Implementation.Repositories
{
    public class GenericDtoRepositoryResolver<TEntity, TEntityRepository> :
        IGenericDtoRepositoryResolver<TEntity>
        where TEntity : class
        where TEntityRepository : IGenericEntityRepository<TEntity>
    {
        public TEntityRepository EntityRepository { get; set; }

        #region Constructor

        public GenericDtoRepositoryResolver(TEntityRepository repository)
        {
            EntityRepository = repository;
        }

        #endregion Constructor

        #region Query

        public virtual IQueryable<TDto> Get<TDto>(Expression<Func<TEntity, bool>> predicate = null)
        {
            return EntityRepository.Get(predicate).ProjectTo<TDto>();
        }

        #endregion Query

        #region Async

        public virtual async Task<TResultDto> CreateAsync<TDto, TResultDto>(TDto dto)
        {
            var entity = Mapper.Map<TEntity>(dto);

            var createdEntity = await EntityRepository.CreateAsync(entity);

            return Mapper.Map<TResultDto>(createdEntity);
        }

        public virtual async Task<TDto> CreateAsync<TDto>(TDto dto)
        {
            return await CreateAsync<TDto, TDto>(dto);
        }

        public virtual async Task<bool> DeleteAsync<TDto>(TDto dto)
        {
            var entity = Mapper.Map<TEntity>(dto);

            return await EntityRepository.DeleteAsync(entity);
        }

        public virtual async Task<TResultDto> UpdateAsync<TDto, TResultDto>(TDto dto)
        {
            var entity = Mapper.Map<TEntity>(dto);

            var updatedEntity = await EntityRepository.UpdateAsync(entity);

            return Mapper.Map<TResultDto>(updatedEntity);
        }

        public virtual async Task<TDto> UpdateAsync<TDto>(TDto dto)
        {
            return await UpdateAsync<TDto, TDto>(dto);
        }

        #endregion Async

        #region Default

        public virtual TResultDto Create<TDto, TResultDto>(TDto dto)
        {
            var entity = Mapper.Map<TEntity>(dto);

            var createdEntity = EntityRepository.Create(entity);

            return Mapper.Map<TResultDto>(createdEntity);
        }

        public virtual TDto Create<TDto>(TDto dto)
        {
            return Create<TDto, TDto>(dto);
        }

        public virtual TResultDto Update<TDto, TResultDto>(TDto dto)
        {
            var entity = Mapper.Map<TEntity>(dto);

            var updatedEntity = EntityRepository.Update(entity);

            return Mapper.Map<TResultDto>(updatedEntity);
        }

        public virtual TDto Update<TDto>(TDto dto)
        {
            return Update<TDto, TDto>(dto);
        }

        public virtual bool Delete<TDto>(TDto dto)
        {
            var entity = Mapper.Map<TEntity>(dto);

            return EntityRepository.Delete(entity);
        }

        #endregion Default

        #region IDisposable
        public void Dispose()
        {
            EntityRepository.Dispose();
        }
        #endregion
    }
}
