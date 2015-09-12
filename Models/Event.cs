using System;
using System.Collections.Generic;
using Common;

namespace Models
{
    public class Event : IEntity, IDatabaseEntity
    {
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public int Id { get; set; }
        public  virtual bool Deleted { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
        public ICollection<User> RegisteredStudents { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
    }
}