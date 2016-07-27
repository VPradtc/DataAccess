using DataAccess.CoreDto.Model.Kendo.Filtering.Bindings;
using System.Collections.Generic;
using System;
using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Factory.Abstraction;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Factory
{
    public class PrimitiveTypeOperatorBindingFactory : IPrimitiveTypeOperatorBindingFactory
    {
        private Type[] _primitiveTypes = new Type[]
        {
            typeof(DateTime),
            typeof(int),
            typeof(decimal),
            typeof(bool),
            typeof(Guid?),
            typeof(Enum),
            typeof(string),
        };

        public IEnumerable<OperatorBinding> CreateBindings()
        {
            var operatorBindings = new List<OperatorBinding>
            {
                new TypeRestrictedOperatorBinding("eq", new EqualsOperatorExpressionFactory(), _primitiveTypes),
                new TypeRestrictedOperatorBinding("neq", new NotEqualsOperatorExpressionFactory(), _primitiveTypes),
                new TypeRestrictedOperatorBinding("gte", new GreaterThanOrEqualOperatorExpressionFactory(), _primitiveTypes),
                new TypeRestrictedOperatorBinding("gt", new GreaterThanOperatorExpressionFactory(), _primitiveTypes),
                new TypeRestrictedOperatorBinding("lte", new LessThanOrEqualOperatorExpressionFactory(), _primitiveTypes),
                new TypeRestrictedOperatorBinding("lt", new LessThanOperatorExpressionFactory(), _primitiveTypes),
            };

            return operatorBindings;
        }
    }
}
