using System;
using Actions;
using Models;

namespace Action
{
    public class UpdateUserNote : SystemAction<User>, ICrudAction<User>
    {
        public UpdateUserNote(User announcement)
        {
            ActionAgainst = announcement;
        }
    }
}
