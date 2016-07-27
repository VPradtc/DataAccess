using DataAccess.CoreDto.Model.Kendo.Filtering.Bindings;
using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Abstraction;
using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Factory.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Services.OperatorExpression
{
    public class OperatorExpressionFactoryService : IOperatorExpressionFactoryService
    {
        public readonly IEnumerable<OperatorBinding> OperatorBindings;

        public OperatorExpressionFactoryService(IPrimitiveTypeOperatorBindingFactory primitiveTypeFactory,
            IStringOperatorBindingFactory stringFactory)
        {
            var primitiveTypeBindings = primitiveTypeFactory.CreateBindings();
            var stringBindings = stringFactory.CreateBindings();

            OperatorBindings = primitiveTypeBindings.Concat(stringBindings);
        }

        public IOperatorExpressionFactory GetExpressionFactory(string @operator, Type propertyType)
        {
            var targetBinding = OperatorBindings.FirstOrDefault(binding => binding.Operator == @operator);

            if (targetBinding == null)
            {
                throw new NotSupportedException($"Operator {@operator} is not supported.");
            }

            if (!targetBinding.IsSupported(propertyType))
            {
                throw new NotSupportedException($"Operator {@operator} does not support type {propertyType.Name}.");
            }

            return targetBinding.ExpressionFactory;
        }
    }
}
