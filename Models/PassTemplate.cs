using System;
using Common;
using Common.Extensions;

namespace Models
{

    public class PassTemplate : DatabaseEntity
    {
        public PassTemplate(int id)
        {
            Id = id;
        }

        public PassTemplate() { }
        
        public virtual string Description { get; set; }
        public virtual string PassType { get; set; }
        public virtual decimal Cost { get; set; }
        public virtual int WeeksValidFor { get; set; }
        public virtual int ClassesValidFor { get; set; }
        public virtual bool AvailableForPurchase { get; set; }

        public override string ToString()
        {
            return this.ToDebugString(nameof(Id), nameof(Description), nameof(PassType), nameof(Cost));
        }
    }
}
