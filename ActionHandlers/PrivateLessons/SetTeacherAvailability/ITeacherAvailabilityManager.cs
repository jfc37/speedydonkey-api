using Models.PrivateLessons;

namespace ActionHandlers.PrivateLessons.SetTeacherAvailability
{
    /// <summary>
    /// Teacher availability manager
    /// </summary>
    public interface ITeacherAvailabilityManager
    {
        /// <summary>
        /// Saves teacher availability
        /// </summary>
        /// <returns></returns>
        TeacherAvailability Save();
    }
}