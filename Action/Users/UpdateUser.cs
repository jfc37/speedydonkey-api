using Actions;
using Models;

namespace Action.Users
{
    public class UpdateUser : SystemAction<User>, ICrudAction<User>
    {
        public UpdateUser(User user)
        {
            ActionAgainst = user;
        }
    }
}