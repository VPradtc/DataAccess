using System;
using System.Linq.Expressions;
using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Model;
using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Strategy.Abstraction;
using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Strategy.Abstraction.Core;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Strategy
{
    public class OwnPropertyAccessStrategy : PropertyAccessStrategy, IOwnPropertyAccessStrategy
    {
        protected override PropertyAccessResult AccessMember(Expression entity, Type entityType, string propertyName)
        {
            var propertyInfo = entityType.GetProperty(propertyName);

            if (propertyInfo == null)
            {
                return null;
            }

            var propertyAccess = Expression.MakeMemberAccess(entity, propertyInfo);

            var result = new PropertyAccessResult(
                propertyType: propertyInfo.PropertyType,
                propertyAccessExpression: propertyAccess);

            return result;
        }
    }
}
