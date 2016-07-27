using System.Linq.Expressions;
using System.Reflection;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Abstraction
{
    public abstract class MethodCallOperatorExpressionFactory : IMethodCallOperatorExpressionFactory
    {
        public virtual Expression Create(Expression left, Expression right)
        {
            return Expression.Call(
                instance: left,
                method: GetTargetMethod(),
                arguments: right);
        }

        public abstract MethodInfo GetTargetMethod();
    }
}
