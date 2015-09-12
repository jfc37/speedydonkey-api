using System;
using System.Collections.Generic;
using Common;

namespace Models
{
    public class Booking : IEntity, IDatabaseEntity
    {
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public virtual Room Room { get; set; }
        public virtual Event Event { get; set; }
        public virtual int Id { get; set; }
        public  virtual bool Deleted { get; set; }
    }
}