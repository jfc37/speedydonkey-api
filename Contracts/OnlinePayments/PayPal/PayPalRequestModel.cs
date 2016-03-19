using System.ComponentModel.DataAnnotations;
using Models.OnlinePayments;

namespace Contracts.OnlinePayments.PayPal
{
    public class PayPalRequestModel : OnlinePaymentRequestModel
    {
        [Required]
        public string ReturnUrl { get; set; }

        [Required]
        public string CancelUrl { get; set; }

        public override PaymentMethod PaymentMethod => PaymentMethod.PayPal;
    }
}