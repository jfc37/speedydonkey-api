using System;
using System.Collections.Generic;

namespace Models
{
    public interface IEvent
    {
        int Id { get; set; }
        ICollection<ITeacher> Teachers { get; set; }
        ICollection<IUser> RegisteredStudents { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        string Name { get; set; }
    }

    public class Event : IEvent, IEntity, IDatabaseEntity
    {
        public int Id { get; set; }
        public  virtual bool Deleted { get; set; }
        public ICollection<ITeacher> Teachers { get; set; }
        public ICollection<IUser> RegisteredStudents { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
    }
}