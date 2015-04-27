using Actions;
using Models;

namespace ActionHandlers
{
    public class RemoveAsTeacherHandler : IActionHandler<RemoveAsTeacher, User>
    {
        private readonly ITeacherStudentConverter _teacherStudentConverter;

        public RemoveAsTeacherHandler(ITeacherStudentConverter teacherStudentConverter)
        {
            _teacherStudentConverter = teacherStudentConverter;
        }

        public User Handle(RemoveAsTeacher action)
        {
            return _teacherStudentConverter.ToStudent(action.ActionAgainst.Id);
        }
    }
}
