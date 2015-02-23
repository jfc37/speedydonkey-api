using Models;

namespace Actions
{
    public class EnrolStudent : IAction<Student>
    {
        public EnrolStudent(Student student)
        {
            ActionAgainst = student;
        }

        public Student ActionAgainst { get; set; }
    }
}
