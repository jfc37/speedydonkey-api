using System;
using Common.Extensions;

namespace Models
{
    public class Pass : DatabaseEntity
    {
        public Pass()
        {
            
        }

        public Pass(int id)
        {
            Id = id;
        }
        public virtual DateTimeOffset StartDate { get; set; }
        public virtual DateTimeOffset EndDate { get; set; }

        public virtual string PassType
        {
            get { return _passType.ToString(); }
            set { Enum.TryParse(value, true, out _passType); }
        }

        public virtual string PaymentStatus
        {
            get { return _paymentStatus.ToString(); }
            set { Enum.TryParse(value, true, out _paymentStatus); }
        }

        public virtual decimal Cost { get; set; }
        public virtual string Description { get; set; }

        public virtual User Owner { get; set; }
        public virtual PassStatistic PassStatistic { get; set; }

        public virtual bool IsValid()
        {
            var today = DateTime.Now.Date;
            return today >= StartDate && today <= EndDate;
        }

        private PassType _passType;
        private PassPaymentStatus _paymentStatus;

        public virtual void PayForClass()
        {
            PassStatistic.NumberOfClassesAttended++;
        }

        public virtual void RefundForClass()
        {
            PassStatistic.NumberOfClassesAttended--;
        }

        public virtual bool IsFuturePass()
        {
            var today = DateTime.Now.Date;
            return today < StartDate;
        }

        public virtual string Note { get; set; }

        public override string ToString()
        {
            return this.ToDebugString(nameof(Id), nameof(Description));
        }
    }

    public enum PassType
    {
        Invalid,
        Unlimited,
        Clip
    }

    public enum PassPaymentStatus
    {
        Invalid,
        Pending,
        Paid
    }
}
