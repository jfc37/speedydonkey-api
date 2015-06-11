using System;
using Common;

namespace Models
{
    public interface IPassStatistic : IEntity
    {
        IPass Pass { get; set; }
        decimal CostPerClass { get; set; }
        int NumberOfClassesAttended { get; set; }
    }
    public class PassStatistic : IPassStatistic, IDatabaseEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public virtual IPass Pass { get; set; }
        public virtual decimal CostPerClass { get; set; }
        public virtual int NumberOfClassesAttended { get; set; }
        public virtual bool Deleted { get; set; }
    }
}
