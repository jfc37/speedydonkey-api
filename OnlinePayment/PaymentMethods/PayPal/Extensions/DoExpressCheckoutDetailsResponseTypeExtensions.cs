using System.Linq;
using OnlinePayments.Extensions;
using OnlinePayments.PaymentMethods.PayPal.Models;
using PayPal.PayPalAPIInterfaceService.Model;

namespace OnlinePayments.PaymentMethods.PayPal.Extensions
{
    public static class DoExpressCheckoutDetailsResponseTypeExtensions
    {
        public static PayPalCompleteResponse ToResponse(this DoExpressCheckoutPaymentResponseType instance)
        {
            return new PayPalCompleteResponse
            {
                Errors = instance.Errors
                    .Select(x => ErrorTypeExtensions.ToPaypalError(x))
                    .ToList(),
                Status = instance.Ack.ToString()
            };
        }
    }
}