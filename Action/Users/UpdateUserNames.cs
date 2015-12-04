using Actions;
using Models;

namespace Action.Users
{
    public class UpdateUserNames : ICrudAction<User>
    {
        public UpdateUserNames(User user)
        {
            ActionAgainst = user;
        }

        public User ActionAgainst { get; set; }
    }
}
