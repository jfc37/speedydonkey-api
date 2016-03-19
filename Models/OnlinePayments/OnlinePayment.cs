using System;

namespace Models.OnlinePayments
{
    public class OnlinePayment : DatabaseEntity
    {
        public virtual OnlinePaymentItem ItemType { get; set; }
        public virtual string ItemId { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual decimal Price { get; set; }
        public virtual decimal Fee { get; set; }
        public virtual decimal Total => Price + Fee;
        public virtual string Description { get; set; }
        public virtual OnlinePaymentStatus PaymentStatus { get; set; }
        public virtual Guid ReferenceNumber { get; set; }
        public virtual int InitiatedBy { get; set; }
    }

    public enum OnlinePaymentItem
    {
        Pass,
        WindyLindy
    }

    public enum PaymentMethod
    {
        PayPal,
        Poli
    }

    public enum OnlinePaymentStatus
    {
        Pending,
        Complete
    }
}
