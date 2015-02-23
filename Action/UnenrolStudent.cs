using Models;

namespace Actions
{
    public class UnenrolStudent : IAction<Student>
    {
        public UnenrolStudent(Student student)
        {
            ActionAgainst = student;
        }

        public Student ActionAgainst { get; set; }
    }
}
