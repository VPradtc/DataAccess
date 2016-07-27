using DataAccess.CoreDto.Model.Kendo.Filtering.Bindings.Factory;
using DataAccess.CoreDto.Model.Kendo.Filtering.Parsing.Resolver.Model;
using System;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Parsing.Resolver
{
    public class EnumValueTypeResolver : ValueTypeResolver
    {
        private object ResolveEnum(ValueTypeResolutionContext context)
        {
            var enumType = context.DesiredType;

            var parserType = typeof(EnumValueParser<>).MakeGenericType(enumType);

            var parser = Activator.CreateInstance(parserType);

            var parserMethod = parserType.GetMethod("Parse", new Type[] { typeof(string) });

            var result = parserMethod.Invoke(parser, new[] { context.Value });

            return result;
        }

        public override object Resolve(ValueTypeResolutionContext context)
        {
            if (!typeof(Enum).IsAssignableFrom(context.DesiredType))
            {
                return base.Resolve(context);
            }

            return ResolveEnum(context);
        }
    }
}
