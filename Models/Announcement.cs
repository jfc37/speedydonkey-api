using System;
using System.Collections.Generic;

namespace Models
{
    public interface IAnnouncement : IEntity
    {
        string Message { get; set; }
        IEnumerable<IBlock> Receivers { get; set; }
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
        public virtual IEnumerable<IBlock> Receivers { get; set; }
        public virtual string Type { get; set; }
        public virtual DateTime? ShowFrom { get; set; }
        public virtual DateTime? ShowUntil { get; set; }
        public virtual bool NotifyAll { get; set; }
        public virtual bool Deleted { get; set; }
    }

    public enum AnnouncementType
    {
        Email,
        Banner
    }
}
