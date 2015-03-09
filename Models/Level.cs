using System;
using System.Collections.Generic;

namespace Models
{
    public interface ILevel
    {
        string Name { get; set; }
        IRoom Room { get; set; }
        IList<ITeacher> Teachers { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        int ClassesInBlock { get; set; }
    }

    public class Level : ILevel, IEntity
    {
        public virtual string Name { get; set; }
        public virtual IRoom Room { get; set; }
        public virtual IList<ITeacher> Teachers { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime EndTime { get; set; }
        public virtual int ClassesInBlock { get; set; }
        public virtual int Id { get; set; }
    }
}