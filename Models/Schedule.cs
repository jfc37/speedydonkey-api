using System.Collections.Generic;

namespace Models
{
    public interface ISchedule
    {
        int Id { get; set; }
        IList<IBooking> Bookings { get; set; }
    }
        
    public class Schedule : ISchedule, IEntity
    {
        public virtual IList<IBooking> Bookings { get; set; }
        public virtual int Id { get; set; }
    }
}