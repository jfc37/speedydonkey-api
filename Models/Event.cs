using System;
using System.Collections.Generic;
using Common;

namespace Models
{
    public class Event : IEntity, IDatabaseEntity
    {
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public virtual int Id { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<User> RegisteredStudents { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime EndTime { get; set; }
        public virtual string Name { get; set; }
    }
}