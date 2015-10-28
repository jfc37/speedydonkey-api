using System;
using System.Collections.Generic;
using Common;

namespace Models
{
    public class Block : IDatabaseEntity, IEntity
    {
        public Block(int id) : this()
        {
            Id = id;
        }

        public Block()
        {
            Classes = new List<Class>();
        }

        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<User> EnroledStudents { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Announcement> Announcements { get; set; }
        public virtual Room Room { get; set; }
        public virtual DateTimeOffset StartDate { get; set; }
        public virtual DateTimeOffset EndDate { get; set; }
        public virtual string Name { get; set; }
        public virtual int Id { get; set; }
        public virtual int NumberOfClasses { get; set; }
        public virtual int MinutesPerClass { get; set; }
    }
}