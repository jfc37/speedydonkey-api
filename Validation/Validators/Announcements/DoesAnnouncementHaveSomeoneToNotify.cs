using Common.Extensions;
using Models;
using Validation.Rules;

namespace Validation.Validators.Announcements
{
    public class DoesAnnouncementHaveSomeoneToNotify : IRule
    {
        private readonly Announcement _announcement;

        public DoesAnnouncementHaveSomeoneToNotify(Announcement announcement)
        {
            _announcement = announcement;
        }

        public bool IsValid()
        {
            return _announcement.NotifyAll || _announcement.Receivers.IsNotNullOrEmpty();
        }
    }
}