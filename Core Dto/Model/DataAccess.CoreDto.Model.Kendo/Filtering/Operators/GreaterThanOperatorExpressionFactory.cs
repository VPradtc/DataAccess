using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Abstraction;
using System.Linq.Expressions;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Operators
{
    public class GreaterThanOperatorExpressionFactory : IOperatorExpressionFactory
    {
        public virtual Expression Create(Expression left, Expression right)
        {
            return Expression.GreaterThan(left, right);
        }
    }
}
