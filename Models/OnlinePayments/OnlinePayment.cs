using System;
using Common;

namespace Models.OnlinePayments
{
    public class OnlinePayment : IDatabaseEntity, IEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public virtual bool Deleted { get; set; }
        public virtual OnlinePaymentItem ItemType { get; set; }
        public virtual string ItemId { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual decimal Price { get; set; }
        public virtual decimal Fee { get; set; }
        public virtual decimal Total { get { return Price + Fee; } }
        public virtual string Description { get; set; }
    }

    public enum OnlinePaymentItem
    {
        Pass,
        WindyLindy
    }

    public enum PaymentMethod
    {
        BankDeposit,
        PayPal
    }
}
