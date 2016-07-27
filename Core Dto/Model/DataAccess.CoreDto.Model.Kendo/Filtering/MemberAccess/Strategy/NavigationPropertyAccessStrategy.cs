using System;
using System.Linq.Expressions;
using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Model;
using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Strategy.Abstraction;
using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Strategy.Abstraction.Core;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Strategy
{
    public class NavigationPropertyAccessStrategy : PropertyAccessStrategy, INavigationPropertyAccessStrategy
    {
        private readonly IOwnPropertyAccessStrategy _ownPropertyAccessStrategy;

        public NavigationPropertyAccessStrategy(IOwnPropertyAccessStrategy ownPropertyAccessStrategy)
        {
            _ownPropertyAccessStrategy = ownPropertyAccessStrategy;
        }

        protected override PropertyAccessResult AccessMember(Expression entity, Type entityType, string propertyName)
        {
            var isNestedProperty = propertyName.Contains(".");

            if (!isNestedProperty)
            {
                return null;
            }

            var propertyParts = propertyName.Split('.');

            var ownPropertyName = propertyParts[0];
            var navigationPropertyName = propertyParts[1];

            var ownProperty = _ownPropertyAccessStrategy.Execute(entity, entityType, ownPropertyName);

            if(ownProperty == null)
            {
                return null;
            }

            var result = _ownPropertyAccessStrategy.Execute(ownProperty.PropertyAccessExpression, ownProperty.PropertyType, navigationPropertyName);

            return result;
        }
    }
}
