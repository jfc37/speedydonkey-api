using Actions;
using Models;

namespace Action
{
    public class ResetPassword : IAction<User>
    {
        public ResetPassword(User user)
        {
            ActionAgainst = user;
        }

        public User ActionAgainst { get; set; }
    }
}
