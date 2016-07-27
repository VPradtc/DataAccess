using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Abstraction;
using System.Linq.Expressions;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Operators.FilterUnionOperators
{
    public class OrUnionOperatorExpressionFactory : IOperatorExpressionFactory
    {
        public virtual Expression Create(Expression left, Expression right)
        {
            return Expression.Or(left, right);
        }
    }
}
