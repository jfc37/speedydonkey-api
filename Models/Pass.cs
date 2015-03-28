using System;

namespace Models
{
    public interface IPass
    {
        int Id { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        string PassType { get; set; }
        string PaymentStatus { get; set; }
        IUser Owner { get; set; }
        bool IsValid();
    }

    public class Pass : IPass, IEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }

        public virtual string PassType
        {
            get { return _passType.ToString(); }
            set { _passType = (PassType)Enum.Parse(typeof(PassType), value, true); }
        }

        public virtual string PaymentStatus
        {
            get { return _paymentStatus.ToString(); }
            set { _paymentStatus = (PassPaymentStatus)Enum.Parse(typeof(PassPaymentStatus), value, true); }
        }

        public virtual IUser Owner { get; set; }
        public virtual bool IsValid()
        {
            var today = DateTime.Now.Date;
            return today >= StartDate && today <= EndDate;
        }

        private PassType _passType;
        private PassPaymentStatus _paymentStatus;

        public virtual void PayForClass() { }
    }

    public enum PassType
    {
        Invalid,
        Unlimited,
        Clip,
        Single
    }

    public enum PassPaymentStatus
    {
        Invalid,
        Pending,
        Paid
    }
}