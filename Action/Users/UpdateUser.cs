using Actions;
using Models;

namespace Action.Users
{
    public class UpdateUser : ICrudAction<User>
    {
        public UpdateUser(User user)
        {
            ActionAgainst = user;
        }

        public User ActionAgainst { get; set; }
    }
}