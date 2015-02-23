using Models;

namespace Actions
{
    public class UpdateCourse : IAction<Course>
    {
        public UpdateCourse(Course course)
        {
            ActionAgainst = course;
        }

        public Course ActionAgainst { get; set; }
    }
}
