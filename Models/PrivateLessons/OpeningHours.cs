using System;
using Common;
using NodaTime;

namespace Models.PrivateLessons
{
    /// <summary>
    /// Opening Hours
    /// </summary>
    /// <seealso cref="Common.IEntity" />
    /// <seealso cref="Models.IDatabaseEntity" />
    public class OpeningHours : IEntity, IDatabaseEntity
    {
        public virtual int Id { get; set; }
        public virtual IsoDayOfWeek Day { get; set; }
        public virtual LocalTime OpeningTime { get; set; }
        public virtual LocalTime ClosingTime { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpeningHours"/> class.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="openingTime">The opening time.</param>
        /// <param name="closingTime">The closing time.</param>
        public OpeningHours(IsoDayOfWeek day, LocalTime openingTime, LocalTime closingTime)
        {
            Day = day;
            OpeningTime = openingTime;
            ClosingTime = closingTime;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpeningHours"/> class.
        /// </summary>
        public OpeningHours() { }
    }
}