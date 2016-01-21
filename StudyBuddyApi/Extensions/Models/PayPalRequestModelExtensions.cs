using Contracts.OnlinePayments.PayPal;
using Models.OnlinePayments;

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
                ItemId = instance.ItemId,
                ItemType = instance.ItemType.GetValueOrDefault(),
                PaymentMethod = PaymentMethod.PayPal
            };
        }
    }
}