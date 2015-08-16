using System;
using Common.Extensions;
using Models;

namespace Data.CodeChunks
{
    /// <summary>
    /// Calculates the end date of a block based on it's start date, how many classes it contains, and the minutes in a class
    /// </summary>
    public class GetBlockEndDate : ICodeChunk<DateTime>
    {
        private readonly DateTime _startDate;
        private readonly Level _level;

        public GetBlockEndDate(DateTime startDate, Level level)
        {
            _startDate = startDate;
            _level = level;
        }

        public DateTime Do()
        {
            return _startDate
                .AddWeeks((_level.ClassesInBlock - 1))
                .AddMinutes(_level.ClassMinutes);
        }
    }
}