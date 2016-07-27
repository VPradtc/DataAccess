using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Model;
using System;
using System.Linq.Expressions;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Strategy.Abstraction.Core
{
    public interface IPropertyAccessStrategy
    {
        PropertyAccessResult Execute(Expression entity, Type entityType, string propertyName);
    }
}
