using Data.Repositories;
using Models;
using Models.PrivateLessons;

namespace ActionHandlers.PrivateLessons.SetTeacherAvailability
{
    /// <summary>
    /// Creates or updates teacher availability
    /// </summary>
    public class SetTeacherAvailabilityHandler : IActionHandler<Action.PrivateLessons.SetTeacherAvailability, TeacherAvailability>
    {
        private readonly ITeacherAvailabilityManagerFactory _teacherAvailabilityManagerFactory;
        private readonly IRepository<Teacher> _teacherRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetTeacherAvailabilityHandler"/> class.
        /// </summary>
        /// <param name="teacherAvailabilityManagerFactory">The teacher availability manager factory.</param>
        /// <param name="teacherRepository"></param>
        public SetTeacherAvailabilityHandler(
            ITeacherAvailabilityManagerFactory teacherAvailabilityManagerFactory,
            IRepository<Teacher> teacherRepository)
        {
            _teacherAvailabilityManagerFactory = teacherAvailabilityManagerFactory;
            _teacherRepository = teacherRepository;
        }

        /// <summary>
        /// Creates or updates teacher availability
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public TeacherAvailability Handle(Action.PrivateLessons.SetTeacherAvailability action)
        {
            action.ActionAgainst.Teacher = _teacherRepository.Get(action.ActionAgainst.Teacher.Id);

            return _teacherAvailabilityManagerFactory.GetManager(action.ActionAgainst)
                .Save();
        }
    }
}