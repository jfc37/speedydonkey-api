using System;
using System.Collections.Generic;
using Common;

namespace Models
{
    public interface IBlock : IEntity
    {
        ICollection<ITeacher> Teachers { get; set; }
        ICollection<IUser> EnroledStudents { get; set; }
        ILevel Level { get; set; }
        ICollection<IClass> Classes { get; set; }
        ICollection<IAnnouncement> Announcements { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        string Name { get; set; }
    }

    public class Block : IBlock, IDatabaseEntity
    {
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public virtual ICollection<ITeacher> Teachers { get; set; }
        public virtual ICollection<IUser> EnroledStudents { get; set; }
        public virtual ILevel Level { get; set; }
        public virtual ICollection<IClass> Classes { get; set; }
        public virtual ICollection<IAnnouncement> Announcements { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual string Name { get; set; }
        public virtual int Id { get; set; }
        public  virtual bool Deleted { get; set; }
    }
}