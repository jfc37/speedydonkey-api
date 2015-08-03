using System;
using System.Linq;
using Models;

namespace Data.CodeChunks
{
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