using Actions;
using Models;

namespace Action
{
    public class EnrolInBlock : IAction<User>
    {
        public EnrolInBlock(User user)
        {
            ActionAgainst = user;
        }

        public User ActionAgainst { get; set; }
    }
}
