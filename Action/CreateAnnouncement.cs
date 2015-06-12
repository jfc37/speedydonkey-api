using System;
using Actions;
using Models;

namespace Action
{
    public class CreateAnnouncement : ICrudAction<Announcement>
    {
        public CreateAnnouncement(Announcement announcement)
        {
            ActionAgainst = announcement;
        }

        public Announcement ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Create announcement {0}", ActionAgainst.Message);
            }
        }
    }
}
