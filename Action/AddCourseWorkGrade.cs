using Models;

namespace Actions
{
    public class AddCourseWorkGrade : IAction<CourseWorkGrade>
    {
        public AddCourseWorkGrade(CourseWorkGrade courseWorkGrade)
        {
            ActionAgainst = courseWorkGrade;
        }

        public CourseWorkGrade ActionAgainst { get; set; }
    }
}
