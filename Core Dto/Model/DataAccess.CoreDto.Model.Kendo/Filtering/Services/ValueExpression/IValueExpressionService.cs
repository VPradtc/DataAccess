using System;
using System.Linq.Expressions;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.Services.ValueExpresssion
{
    public interface IValueExpressionService
    {
        Expression GetValueExpression(string input, Type targetType);
    }
}
