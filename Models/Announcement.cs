using System;
using System.Collections.Generic;
using Common;
using Common.Extensions;

namespace Models
{
    public class Announcement : IEntity, IDatabaseEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public virtual string Message { get; set; }
        public virtual ICollection<Block> Receivers { get; set; }
        public virtual string Type { get; set; }
        public virtual DateTime? ShowFrom { get; set; }
        public virtual DateTime? ShowUntil { get; set; }
        public virtual bool NotifyAll { get; set; }

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
