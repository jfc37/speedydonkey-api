using Actions;
using Models;

namespace Action
{
    /// <summary>
    /// Action for enrolling a user in a set of blocks
    /// </summary>
    /// <seealso cref="User" />
    public class EnrolInBlock : IAction<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnrolInBlock"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public EnrolInBlock(User user)
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
