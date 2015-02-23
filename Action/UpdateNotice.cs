using Models;

namespace Actions
{
    public class UpdateNotice : IAction<Notice>
    {
        public UpdateNotice(Notice notice)
        {
            ActionAgainst = notice;
        }

        public Notice ActionAgainst { get; set; }
    }
}
