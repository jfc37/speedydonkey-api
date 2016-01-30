using Actions;
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
    }
}
