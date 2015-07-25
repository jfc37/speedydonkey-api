using Models.OnlinePayments;
using SpeedyDonkeyApi.Models.OnlinePayments;

namespace SpeedyDonkeyApi.Extensions.Models
{
    public static class OnlinePaymentRequestModelExtensions
    {
        public static OnlinePayment ToRequest(this OnlinePaymentRequestModel instance)
        {
            return new OnlinePayment
            {
                ItemId = instance.ItemId,
                ItemType = instance.ItemType.GetValueOrDefault()
            };
        }
    }
}