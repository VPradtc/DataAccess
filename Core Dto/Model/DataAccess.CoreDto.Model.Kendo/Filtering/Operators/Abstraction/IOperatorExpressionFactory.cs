using System.Linq.Expressions;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Abstraction
{
    public interface IOperatorExpressionFactory
    {
        Expression Create(Expression left, Expression right);
    }
}
