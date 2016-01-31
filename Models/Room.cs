using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Extensions;

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

        public override string ToString()
        {
            return this.ToDebugString(nameof(Id), nameof(Name));
        }
    }

    public static class RoomExtensions
    {
        public static IEnumerable<Event> GetUpcomingSchedule(this Room instance)
        {
            var schedule = instance.Events.Where(x => x.StartTime.IsBetween(DateTime.Today, DateTime.Today.AddWeeks(1)))
                .ToList();

            return schedule;
        }
    }
}