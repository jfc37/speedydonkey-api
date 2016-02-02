using Models.PrivateLessons;

namespace ActionHandlers.PrivateLessons.SetOpeningHours
{
    /// <summary>
    /// Opening Hour manager
    /// </summary>
    public interface IOpeningHourManager
    {
        /// <summary>
        /// Saves an opening hour
        /// </summary>
        /// <returns></returns>
        TimeSlot Save();
    }
}