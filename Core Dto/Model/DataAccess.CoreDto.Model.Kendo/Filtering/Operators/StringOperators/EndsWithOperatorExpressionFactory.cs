using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Abstraction;
using System;
using System.Reflection;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Operators.StringOperators
{
    public class EndsWithOperatorExpressionFactory : MethodCallOperatorExpressionFactory
    {
        public override MethodInfo GetTargetMethod()
        {
            var result = typeof(String).GetMethod("EndsWith", new[] { typeof(string) });

            return result;
        }
    }
}
