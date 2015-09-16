using System;
using Common;

namespace Models
{
    public class PassStatistic : IEntity, IDatabaseEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public virtual Pass Pass { get; set; }
        public virtual decimal CostPerClass { get; set; }
        public virtual int NumberOfClassesAttended { get; set; }
    }
}
