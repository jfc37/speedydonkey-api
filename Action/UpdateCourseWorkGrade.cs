using Models;

namespace Actions
{
    public class UpdateCourseWorkGrade : IAction<CourseWorkGrade>
    {
        public UpdateCourseWorkGrade(CourseWorkGrade courseWorkGrade)
        {
            ActionAgainst = courseWorkGrade;
        }

        public CourseWorkGrade ActionAgainst { get; set; }
    }
}
