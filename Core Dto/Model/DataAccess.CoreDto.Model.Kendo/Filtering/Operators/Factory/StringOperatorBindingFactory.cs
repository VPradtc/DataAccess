using DataAccess.CoreDto.Model.Kendo.Filtering.Bindings;
using System.Collections.Generic;
using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.StringOperators;
using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Factory.Abstraction;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Factory
{
    public class StringOperatorBindingFactory : IStringOperatorBindingFactory
    {
        public IEnumerable<OperatorBinding> CreateBindings()
        {
            var operatorBindings = new List<OperatorBinding>
            {
                new OperatorBinding("startswith", new StartsWithOperatorExpressionFactory() ),
                new OperatorBinding("endswith", new EndsWithOperatorExpressionFactory() ),
                new OperatorBinding("contains", new ContainsOperatorExpressionFactory() ),
                new OperatorBinding("doesnotcontain", new DoesNotContainOperatorExpressionFactory() ),
            };

            return operatorBindings;
        }
    }
}
