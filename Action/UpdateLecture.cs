using Models;

namespace Actions
{
    public class UpdateLecture : IAction<Lecture>
    {
        public UpdateLecture(Lecture lecture)
        {
            ActionAgainst = lecture;
        }

        public Lecture ActionAgainst { get; set; }
    }
}
