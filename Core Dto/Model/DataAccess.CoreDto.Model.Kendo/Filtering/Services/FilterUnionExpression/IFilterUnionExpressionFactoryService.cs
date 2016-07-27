using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Abstraction;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Services.FilterUnionExpression
{
    public interface IFilterUnionExpressionFactoryService
    {
        IOperatorExpressionFactory GetExpressionFactory(string @operator);
    }
}
