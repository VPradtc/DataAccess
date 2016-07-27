using System.Linq;

namespace DataAccess.CoreDto.Model.Kendo.Filtering
{
    public interface IFilterExpressionBuilder<TDto>
    {
        IQueryable<TDto> CreatedFilteredCollection(IQueryable<TDto> collection, KendoGridFilters filters);
    }
}
