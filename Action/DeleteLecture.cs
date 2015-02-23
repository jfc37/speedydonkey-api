using Models;

namespace Actions
{
    public class DeleteLecture : IAction<Lecture>
    {
        public DeleteLecture(Lecture lecture)
        {
            ActionAgainst = lecture;
        }

        public Lecture ActionAgainst { get; set; }
    }
}
