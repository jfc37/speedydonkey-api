using System.Collections.Generic;

namespace Models
{
    public interface IEvent
    {
        IList<ITeacher> Teachers { get; set; } 
        IList<ITeacher> RegisteredStudents { get; set; } 
        IBooking Booking { get; set; }
    }
}