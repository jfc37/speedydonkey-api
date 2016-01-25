using Models.PrivateLessons;

namespace ActionHandlers.PrivateLessons.SetOpeningHours
{
    /// <summary>
    /// Creates or updates opening hours
    /// </summary>
    public class SetOpeningHoursHandler : IActionHandler<Action.OpeningHours.SetOpeningHours, OpeningHours>
    {
        private readonly IOpeningHourManagerFactory _openingHourManagerFactory;

        public SetOpeningHoursHandler(IOpeningHourManagerFactory openingHourManagerFactory)
        {
            _openingHourManagerFactory = openingHourManagerFactory;
        }

        /// <summary>
        /// Creates or updates opening hours
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public OpeningHours Handle(Action.OpeningHours.SetOpeningHours action)
        {
            return _openingHourManagerFactory.GetManager(action.ActionAgainst)
                .Save();
        }
    }
}