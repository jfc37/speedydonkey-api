using Models;

namespace Actions
{
    public class DeleteCourseWorkGrade : IAction<CourseWorkGrade>
    {
        public DeleteCourseWorkGrade(CourseWorkGrade courseWorkGrade)
        {
            ActionAgainst = courseWorkGrade;
        }

        public CourseWorkGrade ActionAgainst { get; set; }
    }
}
