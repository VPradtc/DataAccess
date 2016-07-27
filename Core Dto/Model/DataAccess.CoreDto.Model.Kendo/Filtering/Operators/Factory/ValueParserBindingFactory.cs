using DataAccess.CoreDto.Model.Kendo.Filtering.Bindings;
using System.Collections.Generic;
using System;
using DataAccess.CoreDto.Model.Kendo.Filtering.Bindings.Factory;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Factory
{
    public class ValueParserBindingFactory
    {
        public IEnumerable<ValueParserBinding> CreateBindings()
        {
            var bindings = new List<ValueParserBinding>
            {
                new ValueParserBinding(typeof(bool), new BoolValueParser()),
                new ValueParserBinding(typeof(DateTime), new DateTimeValueParser()),
                new ValueParserBinding(typeof(decimal), new DecimalValueParser()),
                new ValueParserBinding(typeof(Guid?), new GuidValueParser()),
                new ValueParserBinding(typeof(int), new IntValueParser()),
                new ValueParserBinding(typeof(string), new StringValueParser()),
            };

            return bindings;
        }
    }
}
