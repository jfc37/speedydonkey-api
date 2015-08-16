using Models.OnlinePayments;
using SpeedyDonkeyApi.Models.OnlinePayments.Poli;

namespace SpeedyDonkeyApi.Extensions.Models
{
    public static class PoliRequestModelExtensions
    {
        public static PoliPayment ToRequest(this PoliRequestModel instance)
        {
            return new PoliPayment
            {
                ItemId = instance.ItemId,
                ItemType = instance.ItemType.GetValueOrDefault(),
                CancellationUrl = instance.CancellationUrl,
                SuccessUrl = instance.SuccessUrl,
                FailureUrl = instance.FailureUrl,
                PaymentMethod = PaymentMethod.Poli
            };
        }
    }
}