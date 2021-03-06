using Action.Teachers;
using Models;

namespace ActionHandlers.Teachers
{
    public class SetAsTeacherHandler : IActionHandler<SetAsTeacher, Teacher>
    {
        private readonly ITeacherStudentConverter _teacherStudentConverter;

        public SetAsTeacherHandler(ITeacherStudentConverter teacherStudentConverter)
        {
            _teacherStudentConverter = teacherStudentConverter;
        }

        public Teacher Handle(SetAsTeacher action)
        {
            return _teacherStudentConverter.AddAsTeacher(action.ActionAgainst.User.Id);
        }
    }
}
