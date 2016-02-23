using System.Collections.Generic;
using System.Linq;
using Contracts.Teachers;

namespace Contracts.PrivateLessons
{
    /// <summary>
    /// A time slot a certain teacher is available for
    /// </summary>
    public class TeacherAvailabilityModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeacherAvailabilityModel"/> class.
        /// </summary>
        public TeacherAvailabilityModel()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TeacherAvailabilityModel"/> class.
        /// </summary>
        /// <param name="availabilities">The availabilities.</param>
        public TeacherAvailabilityModel(IEnumerable<TimeSlotModel> availabilities)
        {
            Availabilities = availabilities.ToList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TeacherAvailabilityModel"/> class.
        /// </summary>
        /// <param name="availabilities">The availabilities.</param>
        /// <param name="teacher"></param>
        public TeacherAvailabilityModel(IEnumerable<TimeSlotModel> availabilities, TeacherModel teacher)
            : this(availabilities)
        {
            Teacher = teacher;
        }

        public int Id { get; set; }
        public List<TimeSlotModel> Availabilities { get; set; }
        public TeacherModel Teacher { get; set; }
    }
}