using System;
using Actions;
using Models;

namespace Action
{
    public class ResetPassword : SystemAction<User>
    {
        public ResetPassword(User user)
        {
            ActionAgainst = user;
        }
    }
}
