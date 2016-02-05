using Actions;
using Models;

namespace Action.Users
{
    /// <summary>
    /// Action to remove do not email flag on a user
    /// </summary>
    /// <seealso cref="User" />
    public class RemoveDoNotEmailForUser : SystemAction<User>
    {
        public RemoveDoNotEmailForUser(User user)
        {
            ActionAgainst = user;
        }
    }
}