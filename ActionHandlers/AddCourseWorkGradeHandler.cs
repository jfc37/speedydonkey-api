using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class AddCourseWorkGradeHandler : IActionHandler<AddCourseWorkGrade, CourseWorkGrade>
    {
        private readonly ICourseWorkGradeRepository _courseWorkGradeRepository;

        public AddCourseWorkGradeHandler(ICourseWorkGradeRepository courseWorkGradeRepository)
        {
            _courseWorkGradeRepository = courseWorkGradeRepository;
        }

        public CourseWorkGrade Handle(AddCourseWorkGrade action)
        {
            return _courseWorkGradeRepository.Create(action.ActionAgainst);
        }
    }
}
