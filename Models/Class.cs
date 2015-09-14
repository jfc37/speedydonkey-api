using System.Collections.Generic;

namespace Models
{
    public class Class : Event
    {
        public Class() { }

        public Class(int id)
        {
            Id = id;
        }

        public virtual ICollection<User> ActualStudents { get; set; }
        public virtual Block Block { get; set; }
        public virtual ICollection<PassStatistic> PassStatistics { get; set; }
    }
}