using System;
using System.Collections.Generic;
using Common;

namespace Models
{
    public class Block : IDatabaseEntity, IEntity
    {
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<User> EnroledStudents { get; set; }
        public virtual Level Level { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Announcement> Announcements { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual string Name { get; set; }
        public virtual int Id { get; set; }
    }
}