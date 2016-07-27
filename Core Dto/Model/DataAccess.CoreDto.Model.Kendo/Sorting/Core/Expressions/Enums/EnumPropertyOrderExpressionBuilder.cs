using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.StrategyBuilder;
using DataAccess.CoreDto.Model.Kendo.Filtering.Operators;
using DataAccess.CoreDto.Model.Kendo.Sorting.Core.Expressions.Common.Abstraction;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.CoreDto.Model.Kendo.Sorting.Core.Expressions.Enums
{
    public abstract class EnumPropertyOrderExpressionBuilder<TDto, TEnum> : PropertyOrderExpressionBuilder<TDto>, ITypeBoundPropertyOrderExpressionBuilder<TDto>
        where TEnum : struct, IConvertible
    {
        private readonly EqualsOperatorExpressionFactory _factory;

        public Type TargetType => typeof(TEnum);

        public EnumPropertyOrderExpressionBuilder(IPropertyAccessStrategyChainBuilder propertyAccessStrategyBuilder) : base(propertyAccessStrategyBuilder)
        {
            _factory = new EqualsOperatorExpressionFactory();
        }

        public abstract IEnumerable<TEnum> GetEnumOrder();

        private Expression CreateComparisonExpression(Expression property, TEnum value)
        {
            var intValue = value.ToInt32(CultureInfo.CurrentCulture);

            var valueExpression = Expression.Constant(intValue);

            var propertyIntValue = Expression.Convert(property, typeof(int));

            return _factory.Create(propertyIntValue, valueExpression);
        }

        public IEnumerable<Expression> CreateOrderByClauses(Expression property, string sortDirection)
        {
            var enumOrder = sortDirection == "asc"
                ? GetEnumOrder()
                : GetEnumOrder().Reverse();

            var orderByClauses = enumOrder
                .Select(item => CreateComparisonExpression(property, item))
                .ToList();

            return orderByClauses;
        }

        public override IQueryable<TDto> Build(IQueryable<TDto> collection, ParameterExpression entity, Expression property, string direction)
        {
            var sortOrders = CreateOrderByClauses(property, direction)
                .Select(orderClause => Expression.Lambda<Func<TDto, bool>>(orderClause, entity));

            var orderByClause = sortOrders.FirstOrDefault();

            if(orderByClause == null)
            {
                return collection;
            }

            var result = collection.OrderBy(orderByClause);

            foreach (var item in sortOrders.Skip(1))
            {
                result = result.ThenBy(item);
            }

            return result;
        }
    }
}