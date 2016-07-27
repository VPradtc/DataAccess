using DataAccess.CoreDto.Model.Kendo.Filtering.Parsing.Resolver;
using DataAccess.CoreDto.Model.Kendo.Filtering.Parsing.Resolver.Model;
using System;
using System.Linq.Expressions;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Services.ValueExpresssion
{
    public class ValueExpressionService : IValueExpressionService
    {
        private readonly IValueTypeResolver _valueTypeResolver;

        public ValueExpressionService(IValueTypeResolver valueTypeResolver)
        {
            _valueTypeResolver = valueTypeResolver;
        }

        public Expression GetValueExpression(string input, Type targetType)
        {
            var resolutionContext = new ValueTypeResolutionContext(
                value: input,
                desiredType: targetType);

            var parsedValue = _valueTypeResolver.Resolve(resolutionContext);

            return Expression.Constant(parsedValue, targetType);
        }
    }
}
