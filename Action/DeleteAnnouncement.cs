using System;
using Actions;
using Models;

namespace Action
{
    public class DeleteAnnouncement : ICrudAction<Announcement>
    {
        public DeleteAnnouncement(Announcement announcement)
        {
            ActionAgainst = announcement;
        }

        public Announcement ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Delete announcement {0}", ActionAgainst.Message);
            }
        }
    }
}
