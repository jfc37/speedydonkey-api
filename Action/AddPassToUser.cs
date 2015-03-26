using Actions;
using Models;

namespace Action
{
    public class AddPassToUser : IAction<User>
    {
        public AddPassToUser(User user)
        {
            ActionAgainst = user;
        }

        public User ActionAgainst { get; set; }
    }
}
