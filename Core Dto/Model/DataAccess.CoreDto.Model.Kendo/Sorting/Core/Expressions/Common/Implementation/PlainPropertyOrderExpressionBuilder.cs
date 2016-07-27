using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.StrategyBuilder;
using DataAccess.CoreDto.Model.Kendo.Sorting.Core.Expressions.Common.Abstraction;

namespace DataAccess.CoreDto.Model.Kendo.Sorting.Core.Expressions.Common.Implementation
{
    public class PlainPropertyOrderExpressionBuilder<TDto> : PropertyOrderExpressionBuilder<TDto>, IPlainPropertyOrderExpressionBuilder<TDto>
    {
        public PlainPropertyOrderExpressionBuilder(IPropertyAccessStrategyChainBuilder propertyAccessStrategyBuilder) : base(propertyAccessStrategyBuilder)
        {
        }

        public override IQueryable<TDto> Build(IQueryable<TDto> collection, ParameterExpression entity, Expression property, string sortDirection)
        {
            var orderByClause = Expression.Lambda<Func<TDto, dynamic>>(property, entity);

            var result = sortDirection == "asc"
                ? collection.OrderBy(orderByClause)
                : collection.OrderByDescending(orderByClause);

            return result;
        }
    }
} 