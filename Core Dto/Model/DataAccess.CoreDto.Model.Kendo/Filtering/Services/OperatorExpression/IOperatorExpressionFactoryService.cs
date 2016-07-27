using DataAccess.CoreDto.Model.Kendo.Filtering.Operators.Abstraction;
using System;
using System.Linq.Expressions;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Services.OperatorExpression
{
    public interface IOperatorExpressionFactoryService
    {
        IOperatorExpressionFactory GetExpressionFactory(string @operator, Type propertyType);
    }
}
