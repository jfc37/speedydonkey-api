using System.ComponentModel.DataAnnotations;

namespace SpeedyDonkeyApi.Models.OnlinePayments.Poli
{
    public class PoliNudgeModel
    {
        [Required]
        public string Token { get; set; }
    }
}