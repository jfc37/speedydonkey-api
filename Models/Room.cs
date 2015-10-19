using System;
using System.Collections.Generic;
using Common;

namespace Models
{

    public class Room : IEntity, IDatabaseEntity
    {
        public Room()
        {
            
        }
        public Room(int id)
        {
            Id = id;
        }

        public virtual string Name { get; set; }
        public virtual string Location { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
    }
}