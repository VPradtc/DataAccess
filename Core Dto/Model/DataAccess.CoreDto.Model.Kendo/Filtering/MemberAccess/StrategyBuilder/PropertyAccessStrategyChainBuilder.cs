using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Decorator;
using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Strategy.Abstraction;
using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Strategy.Abstraction.Core;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.StrategyBuilder
{
    public class PropertyAccessStrategyChainBuilder : IPropertyAccessStrategyChainBuilder
    {
        private readonly IOwnPropertyAccessStrategy _ownPropertyStrategy;
        private readonly INavigationPropertyAccessStrategy _navigationPropertyStrategy;

        private readonly IPropertyAccessDecorator _decorator;

        public PropertyAccessStrategyChainBuilder(IOwnPropertyAccessStrategy ownPropertyStrategy,
            INavigationPropertyAccessStrategy navigationPropertyStrategy,
            IPropertyAccessDecorator decorator)
        {
            _ownPropertyStrategy = ownPropertyStrategy;
            _navigationPropertyStrategy = navigationPropertyStrategy;
            _decorator = decorator;
        }

        protected virtual IPropertyAccessStrategyChainLink BuildChain()
        {
            var head = _ownPropertyStrategy;

            head.SetSuccessor(_navigationPropertyStrategy);

            return head;
        }

        public IPropertyAccessStrategy Build()
        {
            var head = BuildChain();

            _decorator.SetStrategy(head);

            return _decorator;
        }
    }
}
