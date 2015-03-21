using System;
using System.Collections.Generic;

namespace Models
{
    public interface IEvent
    {
        int Id { get; set; }
        IList<ITeacher> Teachers { get; set; } 
        IList<IUser> RegisteredStudents { get; set; }
        //IBooking Booking { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        string Name { get; set; }
    }

    public class Event : IEvent, IEntity
    {
        public int Id { get; set; }
        public IList<ITeacher> Teachers { get; set; }
        public IList<IUser> RegisteredStudents { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
    }
}