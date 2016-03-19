using System;
using System.Linq;
using Data.Repositories;
using Models.PrivateLessons;

namespace ActionHandlers.PrivateLessons.SetTeacherAvailability
{
    /// <summary>
    /// Creates Teacher Availability
    /// </summary>
    /// <seealso cref="ITeacherAvailabilityManager" />
    public class CreateTeacherAvailabilityManager : ITeacherAvailabilityManager
    {
        private readonly TeacherAvailability _teacherAvailability;
        private readonly IRepository<TeacherAvailability> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTeacherAvailabilityManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="teacherAvailability">The teacher availability.</param>
        public CreateTeacherAvailabilityManager(
            IRepository<TeacherAvailability> repository,
            TeacherAvailability teacherAvailability)
        {
            _repository = repository;
            _teacherAvailability = teacherAvailability;
        }

        /// <summary>
        /// Creates the teacher availability
        /// </summary>
        /// <returns></returns>
        public TeacherAvailability Save()
        {
            return _repository.Create(_teacherAvailability);
        }
    }
}
