using Actions;
using Models;

namespace ActionHandlers
{
    public class RemoveAsTeacherHandler : IActionHandler<RemoveAsTeacher, Teacher>
    {
        private readonly ITeacherStudentConverter _teacherStudentConverter;

        public RemoveAsTeacherHandler(ITeacherStudentConverter teacherStudentConverter)
        {
            _teacherStudentConverter = teacherStudentConverter;
        }

        public Teacher Handle(RemoveAsTeacher action)
        {
            _teacherStudentConverter.ToStudent(action.ActionAgainst.Id);
            return action.ActionAgainst;
        }
    }
}
