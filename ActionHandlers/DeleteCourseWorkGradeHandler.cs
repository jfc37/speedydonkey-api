using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class DeleteCourseWorkGradeHandler : IActionHandler<DeleteCourseWorkGrade, CourseWorkGrade>
    {
        private readonly ICourseWorkGradeRepository _courseWorkGradeRepository;

        public DeleteCourseWorkGradeHandler(ICourseWorkGradeRepository courseWorkGradeRepository)
        {
            _courseWorkGradeRepository = courseWorkGradeRepository;
        }

        public CourseWorkGrade Handle(DeleteCourseWorkGrade action)
        {
            _courseWorkGradeRepository.Delete(action.ActionAgainst);
            return null;
        }
    }
}
