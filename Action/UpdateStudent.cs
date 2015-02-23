using Models;

namespace Actions
{
    public class UpdateStudent : IAction<Student>
    {
        public UpdateStudent(Student student)
        {
            ActionAgainst = student;
        }

        public Student ActionAgainst { get; set; }
    }
}
