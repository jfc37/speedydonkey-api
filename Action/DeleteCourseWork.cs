using Models;

namespace Actions
{
    public class DeleteCourseWork : IAction<CourseWork>
    {
        public DeleteCourseWork(CourseWork courseWork)
        {
            ActionAgainst = courseWork;
        }

        public CourseWork ActionAgainst { get; set; }
    }
}
