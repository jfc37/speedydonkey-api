using Models.PrivateLessons;

namespace ActionHandlers.PrivateLessons.SetOpeningHours
{
    /// <summary>
    /// Factory for OpeningHourManagers
    /// </summary>
    public interface IOpeningHourManagerFactory
    {
        /// <summary>
        /// Constructs the correct type of OpeningHourManager
        /// </summary>
        /// <param name="openingHours">The opening hours.</param>
        /// <returns></returns>
        IOpeningHourManager GetManager(OpeningHours openingHours);
    }
}