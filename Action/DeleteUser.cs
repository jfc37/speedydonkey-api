using System;
using Actions;
using Models;

namespace Action
{
    public class DeleteUser : SystemAction<User>, ICrudAction<User>
    {
        public DeleteUser(User user)
        {
            ActionAgainst = user;
        }
    }
}
