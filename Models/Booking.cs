using System;
using System.Collections.Generic;

namespace Models
{
    public interface IBooking
    {
        IRoom Room { get; set; }
        IEvent Event { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        IList<ITeacher> Teachers { get; set; } 
        IList<IUser> Students { get; set; } 
    }
}