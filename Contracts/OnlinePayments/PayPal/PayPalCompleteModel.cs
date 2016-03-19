using System.ComponentModel.DataAnnotations;

namespace Contracts.OnlinePayments.PayPal
{
    public class PayPalCompleteModel
    {
        [Required]
        public string Token { get; set; }
    }
}