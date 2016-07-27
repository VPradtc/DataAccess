using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Model;
using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Strategy.Abstraction.Core;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Decorator
{
    public interface IPropertyAccessDecorator : IPropertyAccessStrategy
    {
        void SetStrategy(IPropertyAccessStrategy strategy);
        PropertyAccessResult Decorate(PropertyAccessResult propertyAccess);
    }
}
