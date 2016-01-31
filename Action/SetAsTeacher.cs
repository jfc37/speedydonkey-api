using System;
using Models;

namespace Actions
{
    public class SetAsTeacher : SystemAction<Teacher>, ICrudAction<Teacher>
    {
        public SetAsTeacher(Teacher teacher)
        {
            ActionAgainst = teacher;
        }
    }
}
