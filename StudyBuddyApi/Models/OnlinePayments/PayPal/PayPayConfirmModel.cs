using System.ComponentModel.DataAnnotations;

namespace SpeedyDonkeyApi.Models.OnlinePayments.PayPal
{
    public class PayPayConfirmModel
    {
        [Required]
        public string Token { get; set; }
    }
}