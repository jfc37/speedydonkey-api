using System.ComponentModel.DataAnnotations;

namespace SpeedyDonkeyApi.Models.OnlinePayments.PayPal
{
    public class PayPayCompleteModel
    {
        [Required]
        public string Token { get; set; }
    }
}