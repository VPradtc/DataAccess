using System.Collections.Generic;
using System.Linq;
using DataAccess.CoreDto.Model.Kendo.Filtering.Parsing.Resolver.Model;
using DataAccess.CoreDto.Model.Kendo.Filtering.Bindings;
using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Factory;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Parsing.Resolver
{
    public class ValueTypeResolver : IValueTypeResolver
    {
        public readonly IEnumerable<ValueParserBinding> ValueParserBindings;

        public ValueTypeResolver()
        {
            ValueParserBindings = new ValueParserBindingFactory().CreateBindings();
        }

        public virtual object Resolve(ValueTypeResolutionContext context)
        {
            var parserBinding = ValueParserBindings.First(binding => binding.Type.IsAssignableFrom(context.DesiredType));

            return parserBinding.Parser.Parse(context.Value);
        }
    }
}
