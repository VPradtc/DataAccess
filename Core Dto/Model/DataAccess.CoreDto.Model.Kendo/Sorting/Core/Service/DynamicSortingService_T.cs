using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.StrategyBuilder;
using DataAccess.CoreDto.Model.Kendo.Sorting.Core.ServiceLocator;
using System.Linq.Expressions;
using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Strategy.Abstraction.Core;
using DataAccess.Core.Extensions.System;
using System.Linq;

namespace DataAccess.CoreDto.Model.Kendo.Sorting.Core.Service
{
    public class DynamicSortingService<TEntity> : IDynamicSortingService<TEntity>
    {
        private readonly IPropertyOrderExpressionBuilderLocator<TEntity> _orderExpressionBuilderLocator;
        private readonly IPropertyAccessStrategy _propertyAccessStrategy;

        public DynamicSortingService(IPropertyOrderExpressionBuilderLocator<TEntity> orderExpressionBuilderLocator,
            IPropertyAccessStrategyChainBuilder propertyAccessStrategyBuilder)
        {
            _orderExpressionBuilderLocator = orderExpressionBuilderLocator;

            _propertyAccessStrategy = propertyAccessStrategyBuilder.Build();
        }

        public IQueryable<TEntity> OrderBy(IQueryable<TEntity> collection, string fieldName, string direction)
        {
            var entityType = (typeof(TEntity));

            var sanitizedFieldName = fieldName.ToTitleCase();

            var propertyAccessResult = _propertyAccessStrategy.Execute(Expression.Parameter(entityType), entityType, sanitizedFieldName);

            var expressionBuilder = _orderExpressionBuilderLocator.GetOrderExpressionBuilder(propertyAccessResult.PropertyType);

            var orderExpression = expressionBuilder.Build(collection, fieldName, direction);

            return orderExpression;
        }
    }
}
