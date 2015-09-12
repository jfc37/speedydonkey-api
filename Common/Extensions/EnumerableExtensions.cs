using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool DoesNotHaveSameItemIds<T>(this IEnumerable<T> instance, IEnumerable<T> toCompare) where T : IEntity
        {
            var orginalIds = instance.Select(x => x.Id);
            var updatedIds = toCompare.Select(x => x.Id);
            var hasSameNumber = instance.Count() == toCompare.Count();
            var areSameIds = orginalIds.All(updatedIds.Contains);

            return !hasSameNumber || !areSameIds; 
        }
        public static bool HasSameItems<T>(this IEnumerable<T> instance, IEnumerable<T> toCompare)
        {
            return instance.OrderBy(t => t).SequenceEqual(toCompare.OrderBy(t => t));
        }

        public static bool NotAny<T>(this IEnumerable<T> instatnce)
        {
            return !instatnce.Any();
        }

        public static IEnumerable<TResult> SelectIfNotNull<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source.IsNull())
                return null;

            return source.Select(selector);
        }

        public static List<TSource> ToListIfNotNull<TSource>(this IEnumerable<TSource> source)
        {
            if (source.IsNull())
                return null;
            return source.ToList();
        }
    }
}
