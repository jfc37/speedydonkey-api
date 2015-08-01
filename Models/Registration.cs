using System;
using Common;
using Models.OnlinePayments;

namespace Models
{
    public interface IRegistration : IEntity
    {
        Guid RegistationId { get; set; }
        decimal Amount { get; set; }
        OnlinePaymentStatus PaymentStatus { get; }

    }

    public class Registration : IRegistration, IDatabaseEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public virtual Guid RegistationId { get; set; }
        public virtual decimal Amount { get; set; }

        public virtual bool Deleted { get; set; }

        public virtual OnlinePaymentStatus PaymentStatus { get; set; }
    }

}
