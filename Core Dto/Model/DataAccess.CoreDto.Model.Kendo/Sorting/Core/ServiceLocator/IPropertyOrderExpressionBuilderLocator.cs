using DataAccess.CoreDto.Model.Kendo.Sorting.Core.Expressions.Common.Abstraction;
using System;

namespace DataAccess.CoreDto.Model.Kendo.Sorting.Core.ServiceLocator
{
    public interface IPropertyOrderExpressionBuilderLocator<TDto>
    {
        IPropertyOrderExpressionBuilder<TDto> GetOrderExpressionBuilder(Type propertyType);
    }
}
