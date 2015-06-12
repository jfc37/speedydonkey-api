using System;
using System.Collections.Generic;
using Common;
using Common.Extensions;

namespace Models
{
    public interface IAnnouncement : IEntity
    {
        string Message { get; set; }
        ICollection<IBlock> Receivers { get; set; }
        string Type { get; set; }
        DateTime? ShowFrom { get; set; }
        DateTime? ShowUntil { get; set; }
        bool NotifyAll { get; set; }
        
    }
    public class Announcement : IAnnouncement, IDatabaseEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public virtual string Message { get; set; }
        public virtual ICollection<IBlock> Receivers { get; set; }
        public virtual string Type { get; set; }
        public virtual DateTime? ShowFrom { get; set; }
        public virtual DateTime? ShowUntil { get; set; }
        public virtual bool NotifyAll { get; set; }
        public virtual bool Deleted { get; set; }

        public virtual bool ShouldShowBanner()
        {
            var isABannerNotification = Type.EqualsEnum(AnnouncementType.Banner);
            var isWithinShowingPeriod = ShowFrom.IsLessThan(DateTime.Now) && ShowUntil.IsGreaterThan(DateTime.Now);
            return isABannerNotification && isWithinShowingPeriod;
        }
    }

    public enum AnnouncementType
    {
        Email,
        Banner
    }
}
