using System;
using Common.Extensions;
using Models;

namespace Actions
{
    public class CreateUser : SystemAction<User>, ICrudAction<User>
    {
        public CreateUser(User user)
        {
            ActionAgainst = user;
        }
    }
}
