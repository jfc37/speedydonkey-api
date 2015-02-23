using Models;

namespace Actions
{
    public class CreateNotice : IAction<Notice>
    {
        public CreateNotice(Notice notice)
        {
            ActionAgainst = notice;
        }

        public Notice ActionAgainst { get; set; }
    }
}
