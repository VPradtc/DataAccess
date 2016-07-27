using System.Linq;

namespace DataAccess.CoreDto.Model.Kendo.Sorting.Core.Service
{
    public interface IDynamicSortingService<TEntity>
    {
        IQueryable<TEntity> OrderBy(IQueryable<TEntity> collection, string fieldName, string direction);
    }
}