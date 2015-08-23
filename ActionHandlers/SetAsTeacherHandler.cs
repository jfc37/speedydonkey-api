using Actions;
using Models;

namespace ActionHandlers
{
    public class SetAsTeacherHandler : IActionHandler<SetAsTeacher, User>
    {
        private readonly ITeacherStudentConverter _teacherStudentConverter;

        public SetAsTeacherHandler(ITeacherStudentConverter teacherStudentConverter)
        {
            _teacherStudentConverter = teacherStudentConverter;
        }

        public User Handle(SetAsTeacher action)
        {
            _teacherStudentConverter.AddAsTeacher(action.ActionAgainst.Id);
            return action.ActionAgainst;
        }
    }

}
