using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Abstraction;
using System.Linq.Expressions;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Operators
{
    public class LessThanOrEqualOperatorExpressionFactory : IOperatorExpressionFactory
    {
        public virtual Expression Create(Expression left, Expression right)
        {
            return Expression.LessThanOrEqual(left, right);
        }
    }
}
