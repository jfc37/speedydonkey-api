using System.Collections.Generic;
using System.Linq;
using Models;

namespace Data.QueryFilters
{
    public class AvailableBlocksForEnrolmentFilter : IQueryFilter<Block>
    {
        public IEnumerable<Block> Filter(IEnumerable<Block> query)
        {
            return query.Where(x => !x.IsInviteOnly);
        }
    }
}