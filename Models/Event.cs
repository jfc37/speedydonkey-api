using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;

namespace Models
{
    public abstract class Event : DatabaseEntity
    {
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<User> RegisteredStudents { get; set; }
        public virtual ICollection<User> ActualStudents { get; set; }
        public virtual DateTimeOffset StartTime { get; set; }
        public virtual DateTimeOffset EndTime { get; set; }
        public virtual string Name { get; set; }
        public virtual int ClassCapacity { get; set; }
        public virtual Room Room { get; set; }

        public override string ToString()
        {
            return this.ToDebugString(nameof(Id), nameof(Name), nameof(StartTime), nameof(EndTime));
        }

        /// <summary>
        /// Gets a set of payslips dictating who should be paid how much for teaching this event
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<TeacherEventPaySlip> GetPaySlips();
    }

    public class TeacherEventPaySlip
    {
        public TeacherEventPaySlip(Teacher teacher, decimal pay)
        {
            Teacher = teacher;
            Pay = pay;
        }

        public Teacher Teacher { get; }
        public decimal Pay { get; }
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
        public virtual decimal TeacherRate { get; set; }

        /// <summary>
        /// Gets a set of payslips dictating who should be paid how much for teaching this event
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<TeacherEventPaySlip> GetPaySlips()
        {
            return Teachers.Select(x => new TeacherEventPaySlip(x, TeacherRate));
        }
    }
}