using System;
using System.Collections.Generic;

namespace Models
{
    public interface IBooking
    {
        int Id { get; set; }
        IRoom Room { get; set; }
        IEvent Event { get; set; }
    }

    public class Booking : IBooking, IEntity
    {
        public virtual IRoom Room { get; set; }
        public virtual IEvent Event { get; set; }
        public virtual int Id { get; set; }
    }
}