using System.ComponentModel.DataAnnotations;

namespace SpeedyDonkeyApi.Models
{
    public class PayPalConfirmViewModel
    {
        [Required]
        public string Token { get; set; }
    }
}