using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Strategy.Abstraction.Core;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.StrategyBuilder
{
    public interface IPropertyAccessStrategyChainBuilder
    {
        IPropertyAccessStrategy Build();
    }
}
