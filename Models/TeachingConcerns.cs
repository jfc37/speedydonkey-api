using System.Collections.Generic;

namespace Models
{
    public interface ITeachingConcerns : IEntity
    {
        IList<IEvent> EventsRun { get; set; } 
    }

    public class TeachingConcerns : ITeachingConcerns, IDatabaseEntity
    {
        public virtual int Id { get; set; }
        public virtual bool Deleted { get; set; }
        public virtual IList<IEvent> EventsRun { get; set; }
    }
}
