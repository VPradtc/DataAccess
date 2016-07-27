using DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Model;
using System;
using System.Linq.Expressions;

namespace DataAccess.CoreDto.Model.Kendo.Filtering.MemberAccess.Strategy.Abstraction.Core
{
    public abstract class PropertyAccessStrategy : IPropertyAccessStrategyChainLink
    {
        public IPropertyAccessStrategyChainLink Successor { get; private set; }

        protected abstract PropertyAccessResult AccessMember(Expression entity, Type entityType, string propertyName);

        public PropertyAccessResult Execute(Expression entity, Type entityType, string propertyName)
        {
            var result = AccessMember(entity, entityType, propertyName);

            if (result != null)
            {
                return result;
            }

            if (Successor != null)
            {
                return Successor.Execute(entity, entityType, propertyName);
            }

            return null;
        }

        public IPropertyAccessStrategyChainLink SetSuccessor(IPropertyAccessStrategyChainLink successor)
        {
            Successor = successor;

            return Successor;
        }
    }
}
