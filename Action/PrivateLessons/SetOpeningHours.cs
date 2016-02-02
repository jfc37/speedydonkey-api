using Actions;

namespace Action.PrivateLessons
{
    /// <summary>
    /// Action for setting opening hours.
    /// </summary>
    public class SetOpeningHours : SystemAction<Models.PrivateLessons.TimeSlot>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetOpeningHours"/> class.
        /// </summary>
        /// <param name="actionAgainst">The action against.</param>
        public SetOpeningHours(Models.PrivateLessons.TimeSlot actionAgainst)
        {
            ActionAgainst = actionAgainst;
        }
    }
}