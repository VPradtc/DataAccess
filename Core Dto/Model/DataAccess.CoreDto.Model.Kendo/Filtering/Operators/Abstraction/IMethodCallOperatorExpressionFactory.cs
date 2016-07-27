using System.Reflection;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Abstraction
{
    public interface IMethodCallOperatorExpressionFactory : IOperatorExpressionFactory
    {
        MethodInfo GetTargetMethod();
    }
}
