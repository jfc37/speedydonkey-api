using System.ComponentModel.DataAnnotations;
using Models.OnlinePayments;

namespace SpeedyDonkeyApi.Models.OnlinePayments.Poli
{
    public class PoliRequestModel : OnlinePaymentRequestModel
    {
        public override PaymentMethod PaymentMethod
        {
            get { return PaymentMethod.Poli; }
        }

        [Required]
        public string SuccessUrl { get; set; }
        [Required]
        public string FailureUrl { get; set; }
        [Required]
        public string CancellationUrl { get; set; }
    }
}