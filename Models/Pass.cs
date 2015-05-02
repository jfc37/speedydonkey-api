using System;

namespace Models
{
    public interface IPass : IEntity
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        string PassType { get; set; }
        string PaymentStatus { get; set; }
        decimal Cost { get; set; }
        IUser Owner { get; set; }
        IPassStatistic PassStatistic { get; set; }
        bool IsValid();
    }

    public class Pass : IPass, IDatabaseEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public  virtual bool Deleted { get; set; }
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

        public virtual IUser Owner { get; set; }
        public virtual IPassStatistic PassStatistic { get; set; }

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
            PassStatistic.CostPerClass = Cost/PassStatistic.NumberOfClassesAttended;
        }

        public virtual void RefundForClass()
        {
            PassStatistic.NumberOfClassesAttended--;
            PassStatistic.CostPerClass = Cost / PassStatistic.NumberOfClassesAttended;
        }

        public virtual bool IsFuturePass()
        {
            var today = DateTime.Now.Date;
            return today < StartDate;
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