using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using Data.CodeChunks;
using Models;

namespace SpeedyDonkeyApi.CodeChunks
{
    public class GetUpcomingSchedule : ICodeChunk<IEnumerable<Event>>
    {
        private readonly IEnumerable<Event> _schedule;

        public GetUpcomingSchedule(IEnumerable<Event> schedule)
        {
            _schedule = schedule;
        }

        public IEnumerable<Event> Do()
        {
            var today = DateTime.UtcNow.Date;
            return _schedule
                .Where(x => DateTimeOffsetExtensions.IsOnOrAfter((DateTimeOffset) x.StartTime, today) && DateTimeOffsetExtensions.IsBefore((DateTimeOffset) x.StartTime, today.AddWeeks(1)))
                .ToList();
        }
    }
}