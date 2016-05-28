using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class ListExtensions
    {
        public static void AddRange<T>(this IList<T> instance, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                instance.Add(item);
            }
        }
    }

    public static class EnumerableExtensions
    {
        /// <summary>
        /// Creates a list of numbers from 1, up to the specified number.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static IEnumerable<int> ToNumberRange(this int instance)
        {
            return Enumerable.Range(1, instance);
        }

        /// <summary>
        /// For each on an IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="action">The action.</param>
        public static void Each<T>(this IEnumerable<T> instance, Action<T> action)
        {
            instance.ToList()
                .ForEach(action);
        }

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

        public static bool IsEmpty<T>(this IEnumerable<T> instance)
        {
            return instance.NotAny();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> instance)
        {
            return instance.IsNull() || instance.NotAny();
        }

        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> instance)
        {
            return !instance.IsNullOrEmpty();
        }

        public static bool NotAny<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return !source.Any(predicate);
        }

        public static string JoinToString<T>(this IEnumerable<T> instance, string separator)
        {
            return string.Join(separator, instance);
        }
    }
}
