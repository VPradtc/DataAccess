using System.Linq;

namespace DataAccess.CoreDto.Model.Kendo.Sorting.Core.Expressions.Common.Abstraction
{
    public interface IPropertyOrderExpressionBuilder<TDto>
    {
        IQueryable<TDto> Build(IQueryable<TDto> collection, string fieldName, string direction);
    }
}