using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Core.Extensions.System.Linq
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<TResult> FullOuterJoin<TOuter, TInner, TKey, TResult>(
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TKey, TResult> resultSelector,
            TOuter defaultInnerElement = default(TOuter),
            TInner defaultOuterElement = default(TInner),
            IEqualityComparer<TKey> comparer = null)
        {
            comparer = comparer ?? EqualityComparer<TKey>.Default;

            var outerLookup = outer.ToLookup(outerKeySelector, comparer);
            var innerLookup = inner.ToLookup(innerKeySelector, comparer);

            var keys = new HashSet<TKey>(outerLookup.Select(element => element.Key), comparer);
            keys.UnionWith(innerLookup.Select(element => element.Key));

            var join = from key in keys
                       from outerElement in outerLookup[key].DefaultIfEmpty(defaultInnerElement)
                       from innerElement in innerLookup[key].DefaultIfEmpty(defaultOuterElement)
                       select resultSelector(outerElement, innerElement, key);

            return join;
        }
    }
}
