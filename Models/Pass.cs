using System;
using Common;

namespace Models
{
    public class Pass : IEntity, IDatabaseEntity
    {
        public Pass()
        {
            
        }

        public Pass(int id)
        {
            Id = id;
        }
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }

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
