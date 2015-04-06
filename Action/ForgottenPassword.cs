using Actions;
using Models;

namespace Action
{
    public class ForgottenPassword : IAction<User>
    {
        public ForgottenPassword(User user)
        {
            ActionAgainst = user;
        }

        public User ActionAgainst { get; set; }
    }
}
