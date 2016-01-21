using System.ComponentModel.DataAnnotations;

namespace Contracts.OnlinePayments.Poli
{
    public class PoliCompleteModel
    {
        [Required]
        public string Token { get; set; }
    }
}