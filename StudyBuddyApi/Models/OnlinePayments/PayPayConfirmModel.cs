using System.ComponentModel.DataAnnotations;

namespace SpeedyDonkeyApi.Models.OnlinePayments
{
    public class PayPayConfirmModel
    {
        [Required]
        public string Token { get; set; }
    }
}