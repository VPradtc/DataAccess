using DataAccess.CoreDto.Model.Kendo.Filtering.Bindings;
using System.Collections.Generic;
using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.FilterUnionOperators;
using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Factory.Abstraction;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Factory
{
    public class FilterUnionOperatorBindingFactory : IFilterUnionOperatorBindingFactory
    {
        public IEnumerable<OperatorBinding> CreateBindings()
        {
            var operatorBindings = new List<OperatorBinding>
            {
                new OperatorBinding("and", new AndUnionOperatorExpressionFactory() ),
                new OperatorBinding("or", new OrUnionOperatorExpressionFactory() ),
            };

            return operatorBindings;
        }
    }
}
