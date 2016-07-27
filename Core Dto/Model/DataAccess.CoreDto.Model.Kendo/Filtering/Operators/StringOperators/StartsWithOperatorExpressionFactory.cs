using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Abstraction;
using System;
using System.Reflection;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Operators.StringOperators
{
    public class StartsWithOperatorExpressionFactory : MethodCallOperatorExpressionFactory
    {
        public override MethodInfo GetTargetMethod()
        {
            var result = typeof(String).GetMethod("StartsWith", new[] { typeof(string) });

            return result;
        }
    }
}
