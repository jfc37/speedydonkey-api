using Actions;
using Models;

namespace Action
{
    /// <summary>
    /// Action for enrolling a user in a set of blocks
    /// </summary>
    /// <seealso cref="User" />
    public class EnrolInBlock : SystemAction<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnrolInBlock"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public EnrolInBlock(User user)
        {
            ActionAgainst = user;
        }
    }

    /// <summary>
    /// Action for unenrolling a user in a block
    /// </summary>
    /// <seealso cref="User" />
    public class UnenrolInBlock : SystemAction<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnenrolInBlock"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public UnenrolInBlock(User user)
        {
            ActionAgainst = user;
        }
    }
}
