using System;
using Common;

namespace Models
{
    public class PassStatistic : DatabaseEntity
    {
        public virtual Pass Pass { get; set; }
        public virtual decimal CostPerClass { get; set; }
        public virtual int NumberOfClassesAttended { get; set; }
    }
}
