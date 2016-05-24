using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Guards the against null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <exception cref="System.InvalidOperationException"></exception>
        public static void GuardAgainstNull<T>(this T instance)
        {
            if (instance.IsNull())
            {
                throw new InvalidOperationException($"Parameter was null: {instance.GetType().Name}");
            }
        }

        public static string ToJson(this object instance)
        {
            return JsonConvert.SerializeObject(instance);
        }

        public static List<T> PutIntoList<T>(this T instance)
        {
            return new List<T> {instance};
        }

        public static bool IsNull(this object instance)
        {
            return instance == null;
        }

        public static bool IsNotNull(this object instance)
        {
            return !instance.IsNull();
        }

        /// <summary>
        /// Determines whether [is of type].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static bool IsOfType<T>(this object instance)
        {
            return instance is T;
        }

        /// <summary>
        /// Determines whether [is not of type].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static bool IsNotOfType<T>(this object instance)
        {
            return !instance.IsOfType<T>();
        }
    }
}
