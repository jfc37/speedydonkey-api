using System;
using System.Collections.Generic;
using Common;
using Common.Extensions;

namespace Models
{
    public abstract class Event : IEntity, IDatabaseEntity
    {
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public virtual int Id { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<User> RegisteredStudents { get; set; }
        public virtual ICollection<User> ActualStudents { get; set; }
        public virtual DateTimeOffset StartTime { get; set; }
        public virtual DateTimeOffset EndTime { get; set; }
        public virtual string Name { get; set; }
        public virtual Room Room { get; set; }

        public override string ToString()
        {
            return this.ToDebugString(nameof(Id), nameof(Name), nameof(StartTime), nameof(EndTime));
        }
    }

    public class StandAloneEvent : Event
    {
        public StandAloneEvent()
        {
            
        }

        public StandAloneEvent(int id)
        {
            Id = id;
        }

        public virtual decimal Price { get; set; }
        public virtual bool IsPrivate { get; set; }
    }
}