using System;

namespace DataAccess.CoreDto.Model.Kendo.Sorting.Core.Expressions.Common.Abstraction
{
    public interface ITypeBoundPropertyOrderExpressionBuilder<TDto> : IPropertyOrderExpressionBuilder<TDto>
    {
        Type TargetType { get; }
    }
}