using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Model;
using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.StrategyBuilder;
using System.Linq;
using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Strategy.Abstraction.Core;
using System.Linq.Expressions;
using DataAccess.Core.Extensions.System;

namespace DataAccess.CoreDto.Model.Kendo.Sorting.Core.Expressions.Common.Abstraction
{
    public abstract class PropertyOrderExpressionBuilder<TDto> : IPropertyOrderExpressionBuilder<TDto>
    {
        private readonly IPropertyAccessStrategy _propertyAccessStrategy;

        public PropertyOrderExpressionBuilder(IPropertyAccessStrategyChainBuilder propertyAccessStrategyBuilder)
        {
            _propertyAccessStrategy = propertyAccessStrategyBuilder.Build();

        }

        public abstract IQueryable<TDto> Build(IQueryable<TDto> collection, ParameterExpression entity, Expression property, string sortDirection);

        protected PropertyAccessResult GetProperty(Expression entity, string propertyName)
        {
            var entityType = typeof(TDto);

            var sanitizedPropertyName = propertyName.ToTitleCase();

            var propertyAccessResult = _propertyAccessStrategy.Execute(entity, entityType, sanitizedPropertyName);

            return propertyAccessResult;
        }

        public IQueryable<TDto> Build(IQueryable<TDto> collection, string fieldName, string direction)
        {
            var entity = Expression.Parameter(typeof(TDto));
            var property = GetProperty(entity, fieldName);

            return Build(collection, entity, property.PropertyAccessExpression, direction);
        }
    }
}