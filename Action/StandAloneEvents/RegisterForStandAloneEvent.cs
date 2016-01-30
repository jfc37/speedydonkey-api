using Actions;
using Models;

namespace Action.StandAloneEvents
{
    /// <summary>
    /// Action for registering a user in stand alone events
    /// </summary>
    public class RegisterForStandAloneEvent : SystemAction<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterForStandAloneEvent"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public RegisterForStandAloneEvent(User user)
        {
            ActionAgainst = user;
        }
    }
}
