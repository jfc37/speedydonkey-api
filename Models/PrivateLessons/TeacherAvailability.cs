using System;
using System.Collections.Generic;
using Common;

namespace Models.PrivateLessons
{
    /// <summary>
    /// The time slots a teacher is available for private lessons
    /// </summary>
    /// <seealso cref="Common.IEntity" />
    /// <seealso cref="Models.IDatabaseEntity" />
    public class TeacherAvailability : DatabaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeacherAvailability"/> class.
        /// </summary>
        public TeacherAvailability()
        {
            CreatedDateTime = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TeacherAvailability"/> class.
        /// </summary>
        /// <param name="availabilities">The availabilities.</param>
        /// <param name="teacher">The teacher.</param>
        public TeacherAvailability(IEnumerable<TimeSlot> availabilities, Teacher teacher)
            : this()
        {
            Availabilities = availabilities;
            Teacher = teacher;
        }
        
        public virtual IEnumerable<TimeSlot> Availabilities { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}