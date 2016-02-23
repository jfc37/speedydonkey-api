namespace Contracts.PrivateLessons
{
    /// <summary>
    /// Model of time
    /// </summary>
    public class LocalTimeModel
    {
        public int Hour { get; set; }
        public int Minute { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalTimeModel"/> class.
        /// </summary>
        public LocalTimeModel()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalTimeModel"/> class.
        /// </summary>
        /// <param name="hour">The hour.</param>
        /// <param name="minute">The minute.</param>
        public LocalTimeModel(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;
        }

        /// <summary>
        /// Adds hour to instance of LocalTimeModel
        /// </summary>
        /// <param name="hours">The hours.</param>
        /// <returns></returns>
        public LocalTimeModel PlusHours(int hours)
        {
            return new LocalTimeModel(Hour + hours, Minute);
        }
    }
}