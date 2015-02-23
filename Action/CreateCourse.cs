using Models;

namespace Actions
{
    public class CreateCourse : IAction<Course>
    {
        public CreateCourse(Course course)
        {
            ActionAgainst = course;
        }

        public Course ActionAgainst { get; set; }
    }
}
