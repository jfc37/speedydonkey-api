using System.Collections.Generic;

namespace Data.QueryFilters
{
    /// <summary>
    /// Filters a query down
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQueryFilter<T>
    {
        /// <summary>
        /// Filters the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IEnumerable<T> Filter(IEnumerable<T> query);
    }
}