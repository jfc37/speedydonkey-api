using System;
using System.Linq;
using Models;

namespace Data.CodeChunks
{
    /// <summary>
    /// Calculates the start date of a new pass for a user
    /// If no current passes exists, will be today.
    /// Otherwise the day after their most future pass
    /// </summary>
    public class GetStartDateForUsersPurchasedPass : ICodeChunk<DateTime>
    {
        private readonly User _user;

        public GetStartDateForUsersPurchasedPass(User user)
        {
            _user = user;
        }

        public DateTime Do()
        {
            var validPasses = _user.Passes
                .OfType<Pass>()
                .Where(x => x.IsValid() || x.IsFuturePass())
                .ToList();
            var startDate = validPasses.Any()
                ? validPasses.Max(x => x.EndDate)
                    .AddDays(1)
                    .Date
                : DateTime.Now.Date;

            return startDate;
        }
    }
}