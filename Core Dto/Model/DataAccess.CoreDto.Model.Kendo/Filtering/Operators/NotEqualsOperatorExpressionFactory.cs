using System.Linq.Expressions;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Operators
{
    public class NotEqualsOperatorExpressionFactory : EqualsOperatorExpressionFactory
    {
        public override Expression Create(Expression left, Expression right)
        {
            var equalsExpression = base.Create(left, right);

            return Expression.Not(equalsExpression);
        }
    }
}
