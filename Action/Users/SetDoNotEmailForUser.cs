using Actions;
using Models;

namespace Action.Users
{
    /// <summary>
    /// Action to set do not email flag on a user
    /// </summary>
    /// <seealso cref="User" />
    public class SetDoNotEmailForUser : SystemAction<User>
    {
        public SetDoNotEmailForUser(User user)
        {
            ActionAgainst = user;
        }
    }
}