using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Abstraction;
using System.Linq.Expressions;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Operators
{
    public class GreaterThanOrEqualOperatorExpressionFactory : IOperatorExpressionFactory
    {
        public virtual Expression Create(Expression left, Expression right)
        {
            return Expression.GreaterThanOrEqual(left, right);
        }
    }
}
