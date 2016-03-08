using Actions;
using Models;

namespace Action.Users
{
    public class CreateUserFromAuthZero : SystemAction<User>, ICrudAction<User>
    {
        public CreateUserFromAuthZero(User user)
        {
            ActionAgainst = user;
        }
    }
}
