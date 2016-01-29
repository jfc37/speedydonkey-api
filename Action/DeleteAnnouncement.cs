using System;
using Actions;
using Models;

namespace Action
{
    public class DeleteAnnouncement : SystemAction<Announcement>, ICrudAction<Announcement>
    {
        public DeleteAnnouncement(Announcement announcement)
        {
            ActionAgainst = announcement;
        }
    }
}
