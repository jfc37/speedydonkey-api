using System.ComponentModel.DataAnnotations;

namespace Contracts.OnlinePayments.Poli
{
    public class PoliNudgeModel
    {
        [Required]
        public string Token { get; set; }
    }
}