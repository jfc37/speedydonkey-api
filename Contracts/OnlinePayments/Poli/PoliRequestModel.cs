using System.ComponentModel.DataAnnotations;
using Models.OnlinePayments;

namespace Contracts.OnlinePayments.Poli
{
    public class PoliRequestModel : OnlinePaymentRequestModel
    {
        public override PaymentMethod PaymentMethod => PaymentMethod.Poli;

        [Required]
        public string SuccessUrl { get; set; }
        [Required]
        public string FailureUrl { get; set; }
        [Required]
        public string CancellationUrl { get; set; }
    }
}