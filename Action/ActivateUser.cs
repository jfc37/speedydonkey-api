using Actions;
using Models;

namespace Action
{
    public class ActivateUser : IAction<User>
    {
        public ActivateUser(User user)
        {
            ActionAgainst = user;
        }

        public User ActionAgainst { get; set; }
    }
}
