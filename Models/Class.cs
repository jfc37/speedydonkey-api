using System;
using System.Collections.Generic;

namespace Models
{
    public interface IClass : IEvent
    {
        IList<IUser> ActualStudents { get; set; }
        IBlock Block { get; set; }

        //has a set of notices
    }

    public class Class : IClass, IEntity
    {
        public virtual int Id { get; set; }
        public virtual IList<ITeacher> Teachers { get; set; }
        public virtual IList<IUser> RegisteredStudents { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime EndTime { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<IUser> ActualStudents { get; set; }
        public virtual IBlock Block { get; set; }
    }
}