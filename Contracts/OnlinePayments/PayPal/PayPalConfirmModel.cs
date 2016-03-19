using System.ComponentModel.DataAnnotations;

namespace Contracts.OnlinePayments.PayPal
{
    public class PayPalConfirmModel
    {
        [Required]
        public string Token { get; set; }
    }
}