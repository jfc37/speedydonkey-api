using System;
using Common.Extensions;
using Common.Extensions.DateTimes;

namespace Validation.Rules
{
    public class DateIsNotTooFarInThePastRule : IRule
    {
        private readonly DateTime _date;
        private const int NumberOfYears = 10;

        public DateIsNotTooFarInThePastRule(DateTime date)
        {
            _date = date;
        }

        public bool IsValid()
        {
            return _date.IsAfter(DateTime.Now.AddYears(-NumberOfYears));
        }
    }
}