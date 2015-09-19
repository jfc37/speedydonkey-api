using System;
using System.Collections.Generic;
using Common;

namespace Models
{

    public class Room : IEntity
    {
        public string Location { get; set; }
        public IList<Booking> Bookings { get; set; }
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
    }
}