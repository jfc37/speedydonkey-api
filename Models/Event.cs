using System;
using System.Collections.Generic;

namespace Models
{
    public interface IEvent
    {
        int Id { get; set; }
        IList<ITeacher> Teachers { get; set; } 
        IList<ITeacher> RegisteredStudents { get; set; }
        IBooking Booking { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
    }
}