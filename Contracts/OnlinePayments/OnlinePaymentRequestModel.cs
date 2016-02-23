using System.ComponentModel.DataAnnotations;
using Models.OnlinePayments;

namespace Contracts.OnlinePayments
{

    public abstract class OnlinePaymentRequestModel
    {
        [Required]
        public OnlinePaymentItem? ItemType { get; set; }

        [Required]
        public string ItemId { get; set; }

        public abstract PaymentMethod PaymentMethod { get; }
    }
}