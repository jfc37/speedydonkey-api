using Actions;
using Models.PrivateLessons;

namespace Action.PrivateLessons
{
    /// <summary>
    /// Action for setting teacher availability.
    /// </summary>
    public class SetTeacherAvailability : SystemAction<TeacherAvailability>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetTeacherAvailability"/> class.
        /// </summary>
        /// <param name="actionAgainst">The action against.</param>
        public SetTeacherAvailability(TeacherAvailability actionAgainst)
        {
            ActionAgainst = actionAgainst;
        }
    }
}