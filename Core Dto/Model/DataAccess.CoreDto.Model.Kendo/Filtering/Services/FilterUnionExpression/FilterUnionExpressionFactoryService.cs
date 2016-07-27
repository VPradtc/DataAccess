using System;
using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Abstraction;
using System.Collections.Generic;
using DataAccess.CoreDto.Model.Kendo.Filtering.Bindings;
using System.Linq;
using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Factory.Abstraction;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Services.FilterUnionExpression
{
    public class FilterUnionExpressionFactoryService : IFilterUnionExpressionFactoryService
    {
        public readonly IEnumerable<OperatorBinding> FilterLogicBindings;

        public FilterUnionExpressionFactoryService(IFilterUnionOperatorBindingFactory factory)
        {
            FilterLogicBindings = factory.CreateBindings();
        }

        public IOperatorExpressionFactory GetExpressionFactory(string @operator)
        {
            var factory = FilterLogicBindings.FirstOrDefault(binding => binding.Operator == @operator).ExpressionFactory;

            if(factory == null)
            {
                throw new NotSupportedException($"Union operator {@operator} is not supported.");
            }

            return factory;
        }
    }
}
