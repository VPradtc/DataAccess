using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Abstraction;
using System;
using System.Linq;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Bindings
{
    public class TypeRestrictedOperatorBinding : OperatorBinding
    {
        public readonly Type[] SupportedTypes;

        public TypeRestrictedOperatorBinding(string @operator, IOperatorExpressionFactory expressionFactory, Type[] supportedTypes)
            : base(@operator, expressionFactory)
        {
            SupportedTypes = supportedTypes;
        }

        public override bool IsSupported(Type targetType)
        {
            return SupportedTypes.Any(type => type.IsAssignableFrom(targetType));
        }
    }
}
