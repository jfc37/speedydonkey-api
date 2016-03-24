using Actions;
using Models;

namespace Action.Users
{
    /// <summary>
    /// Action to set agre to terms on a user
    /// </summary>
    /// <seealso cref="User" />
    public class AgreeToTermsAndConditions : SystemAction<User>
    {
        public AgreeToTermsAndConditions(User user)
        {
            ActionAgainst = user;
        }
    }
}