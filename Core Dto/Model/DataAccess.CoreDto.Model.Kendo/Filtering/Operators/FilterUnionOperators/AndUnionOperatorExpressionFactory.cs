using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Abstraction;
using System.Linq.Expressions;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Operators.FilterUnionOperators
{
    public class AndUnionOperatorExpressionFactory : IOperatorExpressionFactory
    {
        public virtual Expression Create(Expression left, Expression right)
        {
            return Expression.And(left, right);
        }
    }
}
