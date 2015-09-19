using System;
using System.Collections.Generic;
using Common;

namespace Models
{
    public class Level : IEntity, IDatabaseEntity
    {
        public virtual string Name { get; set; }
        public virtual Room Room { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime EndTime { get; set; }
        public virtual int ClassesInBlock { get; set; }
        public virtual IList<Block> Blocks { get; set; }
        public virtual int ClassMinutes { get; set; }
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
    }
}