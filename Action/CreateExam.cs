using Models;

namespace Actions
{
    public class CreateExam : IAction<Exam>
    {
        public CreateExam(Exam exam)
        {
            ActionAgainst = exam;
        }

        public Exam ActionAgainst { get; set; }
    }
}
