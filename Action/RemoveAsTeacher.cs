using System;
using Models;

namespace Actions
{
    public class RemoveAsTeacher : SystemAction<Teacher>, ICrudAction<Teacher>
    {
        public RemoveAsTeacher(Teacher user)
        {
            ActionAgainst = user;
        }
    }
}
