using System.Linq.Expressions;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Operators.StringOperators
{
    public class DoesNotContainOperatorExpressionFactory : ContainsOperatorExpressionFactory
    {
        public override Expression Create(Expression left, Expression right)
        {
            var containsExpression = base.Create(left, right);

            return Expression.Not(containsExpression);
        }
    }
}
