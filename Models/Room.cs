using System.Collections.Generic;

namespace Models
{
    public interface IRoom
    {
        string Location { get; set; }
        IList<IBooking> Bookings { get; set; }
    }

    public class Room : IRoom
    {
        public string Location { get; set; }
        public IList<IBooking> Bookings { get; set; }
    }
}