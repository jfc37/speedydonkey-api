using System.ComponentModel.DataAnnotations;
using Models.OnlinePayments;

namespace SpeedyDonkeyApi.Models.OnlinePayments.PayPal
{
    public class PayPayRequestModel : OnlinePaymentRequestModel
    {
        [Required]
        public string ReturnUrl { get; set; }

        [Required]
        public string CancelUrl { get; set; }

        [Required]
        public string BuyerEmail { get; set; }

        public override PaymentMethod PaymentMethod
        {
            get { return PaymentMethod.PayPal; }
        }
    }
}