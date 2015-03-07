using System.Collections.Generic;

namespace Models
{
    public interface ISchedule
    {
        IList<IBooking> Bookings { get; set; } 
    }
}