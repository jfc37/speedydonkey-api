using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class UpdateCourseWorkGradeHandler : IActionHandler<UpdateCourseWorkGrade, CourseWorkGrade>
    {
        private readonly ICourseWorkGradeRepository _courseWorkGradeRepository;

        public UpdateCourseWorkGradeHandler(ICourseWorkGradeRepository courseWorkGradeRepository)
        {
            _courseWorkGradeRepository = courseWorkGradeRepository;
        }

        public CourseWorkGrade Handle(UpdateCourseWorkGrade action)
        {
            int studentId = action.ActionAgainst.CourseGrade.Student.Id;
            int courseId = action.ActionAgainst.CourseGrade.Course.Id;
            int courseWorkId = action.ActionAgainst.CourseWork.Id;
            var originalAssignment = _courseWorkGradeRepository.Get(studentId, courseId, courseWorkId);
            originalAssignment.GradePercentage = action.ActionAgainst.GradePercentage;
            return _courseWorkGradeRepository.Update(originalAssignment);
        }
    }
}
