using System;
using Actions;
using Models;

namespace Action
{
    public class UpdateAnnouncement : ICrudAction<Announcement>
    {
        public UpdateAnnouncement(Announcement announcement)
        {
            ActionAgainst = announcement;
        }

        public Announcement ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Update announcement {0}", ActionAgainst.Message);
            }
        }
    }
}
