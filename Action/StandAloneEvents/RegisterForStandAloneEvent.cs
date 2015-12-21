using Actions;
using Models;

namespace Action.StandAloneEvents
{
    /// <summary>
    /// Action for registering a user in stand alone events
    /// </summary>
    public class RegisterForStandAloneEvent : IAction<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterForStandAloneEvent"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public RegisterForStandAloneEvent(User user)
        {
            ActionAgainst = user;
        }

        /// <summary>
        /// Gets or sets the action against.
        /// </summary>
        /// <value>
        /// The action against.
        /// </value>
        public User ActionAgainst { get; set; }
    }
}
