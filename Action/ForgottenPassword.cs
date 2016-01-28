using System;
using Actions;
using Models;

namespace Action
{
    public class ForgottenPassword : SystemAction<User>
    {
        public ForgottenPassword(User user)
        {
            ActionAgainst = user;
        }
    }
}
