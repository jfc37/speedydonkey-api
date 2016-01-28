using Actions;
using Models;

namespace Action.Users
{
    public class UpdateUserNames : SystemAction<User>, ICrudAction<User>
    {
        public UpdateUserNames(User user)
        {
            ActionAgainst = user;
        }
    }
}
