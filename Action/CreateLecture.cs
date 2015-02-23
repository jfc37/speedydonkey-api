using Models;

namespace Actions
{
    public class CreateLecture : IAction<Lecture>
    {
        public CreateLecture(Lecture lecture)
        {
            ActionAgainst = lecture;
        }

        public Lecture ActionAgainst { get; set; }
    }
}
