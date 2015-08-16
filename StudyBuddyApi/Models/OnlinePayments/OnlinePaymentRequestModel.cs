using System.ComponentModel.DataAnnotations;
using Models.OnlinePayments;

namespace SpeedyDonkeyApi.Models.OnlinePayments
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