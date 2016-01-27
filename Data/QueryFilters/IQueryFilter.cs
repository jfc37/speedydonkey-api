using System.Collections.Generic;
using Data.Repositories;

namespace Data.QueryFilters
{
    public interface IQueryFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> query);
    }
}