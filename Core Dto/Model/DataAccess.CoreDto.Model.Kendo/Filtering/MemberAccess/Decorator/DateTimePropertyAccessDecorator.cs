using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Model;
using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Strategy.Abstraction.Core;
using System;
using System.Data.Entity.Core.Objects;
using System.Linq.Expressions;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Decorator
{
    public class DateTimePropertyAccessDecorator : IPropertyAccessDecorator
    {
        private IPropertyAccessStrategy _strategy;

        public void SetStrategy(IPropertyAccessStrategy strategy)
        {
            _strategy = strategy;
        }

        public PropertyAccessResult Decorate(PropertyAccessResult propertyAccess)
        {
            if (!typeof(DateTime).IsAssignableFrom(propertyAccess.PropertyType))
            {
                return propertyAccess;
            }

            Func<DateTime?, DateTime?> parserFunc = EntityFunctions.TruncateTime;

            var castedExpression = Expression.Convert(propertyAccess.PropertyAccessExpression, typeof(DateTime?));

            var decoratedPropertyExpression = Expression.Call(parserFunc.Method, castedExpression);

            var result = Expression.Convert(decoratedPropertyExpression, typeof(DateTime));

            return new PropertyAccessResult(
                propertyType: typeof(DateTime),
                propertyAccessExpression: result);
        }

        public PropertyAccessResult Execute(Expression entity, Type entityType, string propertyName)
        {
            if(_strategy == null)
            {
                return null;
            }

            var rawResult = _strategy.Execute(entity, entityType, propertyName);

            if(rawResult == null)
            {
                return null;
            }

            var result = Decorate(rawResult);

            return result;
        }
    }
}
