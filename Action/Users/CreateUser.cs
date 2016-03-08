using Actions;
using Models;

namespace Action.Users
{
    public class CreateUser : SystemAction<User>, ICrudAction<User>
    {
        public CreateUser(User user)
        {
            ActionAgainst = user;
        }
    }
}