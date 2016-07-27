using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Strategy.Abstraction.Core;
using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.StrategyBuilder;
using DataAccess.CoreDto.Model.Kendo.Filtering.Services.FilterUnionExpression;
using DataAccess.CoreDto.Model.Kendo.Filtering.Services.OperatorExpression;
using DataAccess.CoreDto.Model.Kendo.Filtering.Services.ValueExpresssion;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.CoreDto.Model.Kendo.Filtering
{
    public class FilterExpressionBuilder<TDto> : IFilterExpressionBuilder<TDto>
    {
        private readonly IPropertyAccessStrategy _propertyAccessStrategy;
        private readonly IValueExpressionService _valueExpressionService;
        private readonly IOperatorExpressionFactoryService _operatorExpressionFactoryService;
        private readonly IFilterUnionExpressionFactoryService _filterUnionExpressionFactoryService;


        public FilterExpressionBuilder(IPropertyAccessStrategyChainBuilder propertyAccessStrategyBuilder,
            IValueExpressionService valueExpressionService,
            IOperatorExpressionFactoryService operatorExpressionFactoryService,
            IFilterUnionExpressionFactoryService filterUnionExpressionFactoryService)
        {
            _valueExpressionService = valueExpressionService;
            _operatorExpressionFactoryService = operatorExpressionFactoryService;
            _filterUnionExpressionFactoryService = filterUnionExpressionFactoryService;

            _propertyAccessStrategy = propertyAccessStrategyBuilder.Build();
        }

        public IQueryable<TDto> CreatedFilteredCollection(IQueryable<TDto> collection, KendoGridFilters filters)
        {
            var entityType = (typeof(TDto));
            var entity = Expression.Parameter(entityType, "entity");

            var filterUnionExpressionFactory = _filterUnionExpressionFactoryService.GetExpressionFactory(filters.Logic);

            var filterExpressions = filters.Filters.Select(filter => CreateFilterExpression(entity, filter.FormattedField, filter.Operator, filter.Value));

            var whereExpression = filterExpressions.Aggregate((total, current) => filterUnionExpressionFactory.Create(total, current));

            var whereClause = Expression.Lambda<Func<TDto, bool>>(whereExpression, entity);

            return collection.Where(whereClause);
        }

        public Expression CreateFilterExpression(Expression entity, string property, string @operator, string value)
        {
            var propertyAccess = _propertyAccessStrategy.Execute(entity, typeof(TDto), property);

            var propertyType = propertyAccess.PropertyType;
            var propertyExpression = propertyAccess.PropertyAccessExpression;

            var valueExpression = _valueExpressionService.GetValueExpression(value, propertyType);

            var filterExpressionFactory = _operatorExpressionFactoryService.GetExpressionFactory(@operator, propertyType);

            var result = filterExpressionFactory.Create(propertyExpression, valueExpression);

            return result;
        }
    }
}
