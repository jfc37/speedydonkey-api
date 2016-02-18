using Actions;
using Common.Extensions;
using Models;

namespace Action
{
    public class CreateAnnouncement : SystemAction<Announcement>, ICrudAction<Announcement>
    {
        public CreateAnnouncement(Announcement announcement)
        {
            ActionAgainst = announcement;
        }
        
        public int NumberOfUsersEmailed { get; set; }

        public override string ToString()
        {
            return this.ToDebugString(nameof(NumberOfUsersEmailed), nameof(ActionAgainst));
        }
    }
}
