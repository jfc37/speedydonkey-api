using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using Common.Extensions.DateTimes;
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
                .Where(x => x.StartTime.IsOnOrAfter(today) && x.StartTime.IsBefore(today.AddWeeks(1)))
                .ToList();
        }
    }
}