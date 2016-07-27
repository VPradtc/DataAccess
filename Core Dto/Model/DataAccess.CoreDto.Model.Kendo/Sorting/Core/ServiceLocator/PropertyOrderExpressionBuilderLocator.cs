using DataAccess.CoreDto.Model.Kendo.Sorting.Core.Expressions.Common.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.CoreDto.Model.Kendo.Sorting.Core.ServiceLocator
{
    public class PropertyOrderExpressionBuilderLocator<TDto> : IPropertyOrderExpressionBuilderLocator<TDto>
    {
        private readonly IPlainPropertyOrderExpressionBuilder<TDto> _defaultBuilder;

        private readonly List<ITypeBoundPropertyOrderExpressionBuilder<TDto>> _builders;

        public PropertyOrderExpressionBuilderLocator(IPlainPropertyOrderExpressionBuilder<TDto> defaultBuilder)
        {
            _defaultBuilder = defaultBuilder;
            _builders = new List<ITypeBoundPropertyOrderExpressionBuilder<TDto>>
            {
            };
        }

        public IPropertyOrderExpressionBuilder<TDto> GetOrderExpressionBuilder(Type propertyType)
        {
            IPropertyOrderExpressionBuilder<TDto> targetBuilder = _builders.FirstOrDefault(builder => builder.TargetType == propertyType);

            var result = targetBuilder ?? _defaultBuilder;

            return result;
        }
    }
}
