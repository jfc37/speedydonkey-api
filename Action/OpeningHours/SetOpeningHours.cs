using Actions;

namespace Action.OpeningHours
{
    /// <summary>
    /// Action for setting opening hours.
    /// </summary>
    public class SetOpeningHours : IAction<Models.PrivateLessons.OpeningHours>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetOpeningHours"/> class.
        /// </summary>
        /// <param name="actionAgainst">The action against.</param>
        public SetOpeningHours(Models.PrivateLessons.OpeningHours actionAgainst)
        {
            ActionAgainst = actionAgainst;
        }
        public Models.PrivateLessons.OpeningHours ActionAgainst { get; set; }
    }
}