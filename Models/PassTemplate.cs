using System;
using Common;

namespace Models
{

    public class PassTemplate : IEntity, IDatabaseEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public virtual string Description { get; set; }
        public virtual string PassType { get; set; }
        public virtual decimal Cost { get; set; }
        public virtual int WeeksValidFor { get; set; }
        public virtual int ClassesValidFor { get; set; }
        public virtual bool AvailableForPurchase { get; set; }
    }
}
