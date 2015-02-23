using Models;

namespace Actions
{
    public class UpdateExam : IAction<Exam>
    {
        public UpdateExam(Exam exam)
        {
            ActionAgainst = exam;
        }

        public Exam ActionAgainst { get; set; }
    }
}
