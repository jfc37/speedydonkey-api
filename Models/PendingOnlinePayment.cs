using System;
using Common;

namespace Models
{
    public interface IPendingOnlinePayment : IEntity
    {
        string ItemType { get; set; }
        int TemplateId { get; set; }
        string Token { get; set; }
        int UserId { get; set; }
    }

    public class PendingOnlinePayment : IPendingOnlinePayment
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public virtual string ItemType { get; set; }
        public virtual int TemplateId { get; set; }
        public virtual string Token { get; set; }
        public virtual int UserId { get; set; }
    }
}
