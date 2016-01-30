using System;
using Actions;
using Models;

namespace Action
{
    public class ActivateUser : SystemAction<User>
    {
        public ActivateUser(User user)
        {
            ActionAgainst = user;
        }
    }
}
