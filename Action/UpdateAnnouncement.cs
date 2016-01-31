using System;
using Actions;
using Models;

namespace Action
{
    public class UpdateAnnouncement : SystemAction<Announcement>, ICrudAction<Announcement>
    {
        public UpdateAnnouncement(Announcement announcement)
        {
            ActionAgainst = announcement;
        }
    }
}
