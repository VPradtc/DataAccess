
namespace DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Strategy.Abstraction.Core
{
    public interface IPropertyAccessStrategyChainLink : IPropertyAccessStrategy
    {
        IPropertyAccessStrategyChainLink Successor { get; }
        IPropertyAccessStrategyChainLink SetSuccessor(IPropertyAccessStrategyChainLink successor);
    }
}
