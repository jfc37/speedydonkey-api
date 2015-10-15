using System;
using System.Collections.Generic;
using Common;

namespace Models
{
    public class Announcement : IEntity, IDatabaseEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public virtual string Message { get; set; }
        public virtual string Subject { get; set; }
        public virtual ICollection<Block> Receivers { get; set; }
        public virtual bool NotifyAll { get; set; }
    }
}
