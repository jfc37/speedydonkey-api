using System.ComponentModel.DataAnnotations;

namespace SpeedyDonkeyApi.Models.OnlinePayments.PayPal
{
    public class PayPalCompleteModel
    {
        [Required]
        public string Token { get; set; }
    }
}