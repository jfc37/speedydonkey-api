using System.ComponentModel.DataAnnotations;

namespace SpeedyDonkeyApi.Models.OnlinePayments
{
    public class PayPayCompleteModel
    {
        [Required]
        public string Token { get; set; }
    }
}