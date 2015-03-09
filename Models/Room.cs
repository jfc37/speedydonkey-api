using System.Collections.Generic;

namespace Models
{
    public interface IRoom
    {
        string Location { get; set; }
        IList<IBooking> Bookings { get; set; }
    }
}