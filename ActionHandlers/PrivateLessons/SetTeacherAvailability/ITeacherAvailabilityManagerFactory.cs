using Models.PrivateLessons;

namespace ActionHandlers.PrivateLessons.SetTeacherAvailability
{
    /// <summary>
    /// Factory for TeacherAvailabilityManagers
    /// </summary>
    public interface ITeacherAvailabilityManagerFactory
    {
        /// <summary>
        /// Constructs the correct type of TeacherAvailabilityManager
        /// </summary>
        /// <param name="teacherAvailability">The teacher availability.</param>
        /// <returns></returns>
        ITeacherAvailabilityManager GetManager(TeacherAvailability teacherAvailability);
    }
}