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
    public class TimeSlot : DatabaseEntity
    {
        public virtual IsoDayOfWeek Day { get; set; }
        public virtual LocalTime OpeningTime { get; set; }
        public virtual LocalTime ClosingTime { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSlot"/> class.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="openingTime">The opening time.</param>
        /// <param name="closingTime">The closing time.</param>
        public TimeSlot(IsoDayOfWeek day, LocalTime openingTime, LocalTime closingTime)
            : this()
        {
            Day = day;
            OpeningTime = openingTime;
            ClosingTime = closingTime;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSlot"/> class.
        /// </summary>
        public TimeSlot()
        {
            CreatedDateTime = DateTime.Now;
        }
    }
}