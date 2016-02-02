using System.Linq;
using Data.Repositories;
using Models;
using Models.PrivateLessons;

namespace ActionHandlers.PrivateLessons.SetTeacherAvailability
{
    /// <summary>
    /// Factory for TeacherAvailabilityManagers
    /// </summary>
    /// <seealso cref="ITeacherAvailabilityManagerFactory" />
    public class TeacherAvailabilityManagerFactory : ITeacherAvailabilityManagerFactory
    {
        private readonly IRepository<TeacherAvailability> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeacherAvailabilityManagerFactory"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public TeacherAvailabilityManagerFactory(
            IRepository<TeacherAvailability> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Constructs the correct type of TeacherAvailabilityManager
        /// </summary>
        /// <param name="teacherAvailability">The opening hours.</param>
        /// <returns></returns>
        public ITeacherAvailabilityManager GetManager(TeacherAvailability teacherAvailability)
        {
            var alreadyExists = _repository.GetAll()
                .Any(x => x.Teacher.Id == teacherAvailability.Teacher.Id);

            return alreadyExists
                ? (ITeacherAvailabilityManager) new UpdateTeacherAvailabilityManager(_repository, teacherAvailability)
                : new CreateTeacherAvailabilityManager(_repository, teacherAvailability);
        }
    }
}