using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using Models;

namespace Data.QueryFilters
{
    /// <summary>
    /// Filters down to only events available for the public to register for
    /// </summary>
    /// <seealso cref="StandAloneEvent" />
    public class AvailableEventsForRegistrationFilter : IQueryFilter<StandAloneEvent>
    {
        /// <summary>
        /// Filters down to only events available for the public to register for
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>Events available for registration</returns>
        public IEnumerable<StandAloneEvent> Filter(IEnumerable<StandAloneEvent> query)
        {
            var today = DateTime.Today;
            return query
                .Where(x => !x.IsPrivate)
                .Where(x => x.StartTime.Date.IsOnOrAfter(today));
        }
    }
}