using Actions;
using Models;

namespace Action.Teachers
{
    /// <summary>
    /// Action to set a user up to be a teacher
    /// </summary>
    /// <seealso cref="Actions.SystemAction{Models.Teacher}" />
    /// <seealso cref="Actions.ICrudAction{Models.Teacher}" />
    public class SetAsTeacher : SystemAction<Teacher>, ICrudAction<Teacher>
    {
        public SetAsTeacher(Teacher teacher)
        {
            ActionAgainst = teacher;
        }
    }
}