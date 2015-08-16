using System.ComponentModel.DataAnnotations;

namespace SpeedyDonkeyApi.Models.OnlinePayments.Poli
{
    public class PoliCompleteModel
    {
        [Required]
        public string Token { get; set; }
    }
}