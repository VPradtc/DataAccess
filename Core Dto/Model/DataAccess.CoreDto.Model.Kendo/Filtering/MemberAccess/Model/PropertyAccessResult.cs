using System;
using System.Linq.Expressions;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Model
{
    public class PropertyAccessResult
    {
        public readonly Type PropertyType;
        public readonly Expression PropertyAccessExpression;

        public PropertyAccessResult(Type propertyType, Expression propertyAccessExpression)
        {
            PropertyType = propertyType;
            PropertyAccessExpression = propertyAccessExpression;
        }
    }
}
