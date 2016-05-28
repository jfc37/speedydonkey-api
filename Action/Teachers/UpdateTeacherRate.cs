using Actions;
using Models;

namespace Action.Teachers
{
    /// <summary>
    /// Action to update the standard rates a teacher is paid per event
    /// </summary>
    /// <seealso cref="Teacher" />
    /// <seealso cref="Teacher" />
    public class UpdateTeacherRate : SystemAction<Teacher>, ICrudAction<Teacher>
    {
        public UpdateTeacherRate(Teacher teacher)
        {
            ActionAgainst = teacher;
        }
    }
}
