using Models.OnlinePayments;
using SpeedyDonkeyApi.Models.OnlinePayments.PayPal;

namespace SpeedyDonkeyApi.Extensions.Models
{
    public static class PayPalRequestModelExtensions
    {
        public static PayPalPayment ToRequest(this PayPalRequestModel instance)
        {
            return new PayPalPayment
            {
                ReturnUrl = instance.ReturnUrl,
                CancelUrl = instance.CancelUrl,
                BuyerEmail = instance.BuyerEmail,
                ItemId = instance.ItemId,
                ItemType = instance.ItemType.GetValueOrDefault(),
                PaymentMethod = PaymentMethod.PayPal
            };
        }
    }
}