using System.Linq;
using Data.Repositories;
using Models.PrivateLessons;

namespace ActionHandlers.PrivateLessons.SetTeacherAvailability
{
    /// <summary>
    /// Updates opening hours
    /// </summary>
    /// <seealso cref="ITeacherAvailabilityManager" />
    public class UpdateTeacherAvailabilityManager : ITeacherAvailabilityManager
    {
        private readonly TeacherAvailability _teacherAvailability;
        private readonly IRepository<TeacherAvailability> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTeacherAvailabilityManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="teacherAvailability">The opening hours.</param>
        public UpdateTeacherAvailabilityManager(
            IRepository<TeacherAvailability> repository, 
            TeacherAvailability teacherAvailability)
        {
            _repository = repository;
            _teacherAvailability = teacherAvailability;
        }

        /// <summary>
        /// Updates a teachers availability
        /// </summary>
        /// <returns></returns>
        public TeacherAvailability Save()
        {
            var original = _repository.GetAll()
                .Single(x => x.Teacher.Id == _teacherAvailability.Teacher.Id);

            original.Availabilities = _teacherAvailability.Availabilities;

            return _repository.Update(original);
        }
    }
}