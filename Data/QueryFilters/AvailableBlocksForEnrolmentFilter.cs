using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using Models;

namespace Data.QueryFilters
{
    public class AvailableBlocksForEnrolmentFilter : IQueryFilter<Block>
    {
        private readonly DateTime _today;

        public AvailableBlocksForEnrolmentFilter(DateTime today)
        {
            _today = today;
        }

        public IEnumerable<Block> Filter(IEnumerable<Block> query)
        {
            return query
                .Where(x => !x.IsInviteOnly)
                .Where(x => x.EndDate.IsAfter(_today))
                .Where(x => x.StartDate.Date.StartOfWeek(DayOfWeek.Monday).AddWeeks(1).IsAfter(_today));
        }
    }
}