using System.Linq;
using OnlinePayments.PaymentMethods.PayPal.Models;
using PayPal.PayPalAPIInterfaceService.Model;

namespace OnlinePayments.Extensions
{
    public static class SetExpressCheckoutResponseTypeExtensions
    {
        public static StartPayPalPaymentResponse ToResponse(this SetExpressCheckoutResponseType instance)
        {
            return new StartPayPalPaymentResponse
            {
                Token = instance.Token,
                Status = instance.Ack.GetValueOrDefault().ToString(),
                Errors = instance
                    .Errors
                    .Select(x => x.ToPaypalError())
                    .ToList()
            };
        }
    }
}