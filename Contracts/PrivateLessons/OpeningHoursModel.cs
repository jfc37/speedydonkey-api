using NodaTime;

namespace Contracts.PrivateLessons
{
    /// <summary>
    /// The hours the studio is open
    /// </summary>
    public class OpeningHoursModel
    {
        public IsoDayOfWeek Day { get; set; }
        public LocalTimeModel OpeningTime { get; set; }
        public LocalTimeModel ClosingTime { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpeningHoursModel"/> class.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="openingTime">The opening time.</param>
        /// <param name="closingTime">The closing time.</param>
        public OpeningHoursModel(IsoDayOfWeek day, LocalTimeModel openingTime, LocalTimeModel closingTime)
        {
            Day = day;
            OpeningTime = openingTime;
            ClosingTime = closingTime;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpeningHoursModel"/> class.
        /// </summary>
        public OpeningHoursModel() { }
    }
}
