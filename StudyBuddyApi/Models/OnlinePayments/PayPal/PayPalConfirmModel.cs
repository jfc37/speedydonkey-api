using System.ComponentModel.DataAnnotations;

namespace SpeedyDonkeyApi.Models.OnlinePayments.PayPal
{
    public class PayPalConfirmModel
    {
        [Required]
        public string Token { get; set; }
    }
}