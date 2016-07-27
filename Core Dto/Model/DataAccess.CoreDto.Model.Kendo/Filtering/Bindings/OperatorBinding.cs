using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Abstraction;
using System;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Bindings
{
    public class OperatorBinding
    {
        public readonly string Operator;
        public readonly IOperatorExpressionFactory ExpressionFactory;

        public OperatorBinding(string @operator, IOperatorExpressionFactory expressionFactory)
        {
            Operator = @operator;
            ExpressionFactory = expressionFactory;
        }

        public virtual bool IsSupported(Type targetType)
        {
            return true;
        }
    }
}
