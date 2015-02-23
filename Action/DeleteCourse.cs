using Models;

namespace Actions
{
    public class DeleteCourse : IAction<Course>
    {
        public DeleteCourse(Course course)
        {
            ActionAgainst = course;
        }

        public Course ActionAgainst { get; set; }
    }
}
