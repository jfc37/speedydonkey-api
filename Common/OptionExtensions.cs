using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;

namespace Common
{
    public static class OptionExtensions
    {
        /// <summary>
        /// Converts an object into an option
        /// </summary>
        /// <param name="instance">The value.</param>
        /// <returns></returns>
        public static Option<T> ToOption<T>(this T instance)
        {
            return instance.IsNull()
                ? Option<T>.None()
                : Option<T>.Some(instance);
        }

        /// <summary>
        /// Converts a set to an option.
        /// Will excute any underlying SQL.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">Cannot turn list with multiple entries into an option type.</exception>
        public static Option<T> ConvertSetToOption<T>(this IEnumerable<T> instance)
        {
            var list = instance.ToList();

            if (list.Count > 1)
            {
                throw new InvalidOperationException("Cannot turn list with multiple entries into an option type.");
            }

            return list.Select(Option<T>.Some)
                .DefaultIfEmpty(Option<T>.None())
                .Single();
        }
    }
}