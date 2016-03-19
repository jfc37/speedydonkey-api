using System;
using System.Collections.Generic;
using Common;

namespace Models
{
    public class Announcement : DatabaseEntity
    {
        public virtual string Message { get; set; }
        public virtual string Subject { get; set; }
        public virtual ICollection<Block> Receivers { get; set; }
        public virtual bool NotifyAll { get; set; }
    }
}
