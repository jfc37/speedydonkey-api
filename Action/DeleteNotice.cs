using Models;

namespace Actions
{
    public class DeleteNotice : IAction<Notice>
    {
        public DeleteNotice(Notice notice)
        {
            ActionAgainst = notice;
        }

        public Notice ActionAgainst { get; set; }
    }
}
