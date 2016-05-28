using System;

namespace Common.Extensions
{
    public static class DecimalExtensions
    {
        /// <summary>
        /// To the currency string.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static string ToCurrencyString(this decimal instance)
        {
            return instance.ToString("N2");
        }

        /// <summary>
        /// Parses to decimal.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public static decimal ParseToDecimal(this string instance)
        {
            decimal parsedDecimal;

            var canParse = decimal.TryParse(instance, out parsedDecimal);

            if (!canParse)
            {
                throw new InvalidOperationException($"Cannot parse '{instance}' to a decimal.");
            }

            return parsedDecimal;
        }

        /// <summary>
        /// Determines whether this instance is decimal.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static bool IsDecimal(this string instance)
        {
            decimal output;
            return decimal.TryParse(instance, out output);
        }

        /// <summary>
        /// Determines whether [is not decimal].
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static bool IsNotDecimal(this string instance)
        {
            return !instance.IsDecimal();
        }
    }
}