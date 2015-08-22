using System.Linq;
using OnlinePayments.Extensions;
using OnlinePayments.PaymentMethods.PayPal.Models;
using PayPal.PayPalAPIInterfaceService.Model;

namespace OnlinePayments.PaymentMethods.PayPal.Extensions
{
    public static class GetExpressCheckoutDetailsResponseTypeExtensions
    {
        public static PayPalConfirmResponse ToResponse(this GetExpressCheckoutDetailsResponseType instance)
        {
            return new PayPalConfirmResponse
            {
                Token = instance.GetExpressCheckoutDetailsResponseDetails.Token,
                Errors = instance.Errors.Select(x => ErrorTypeExtensions.ToPaypalError(x)),
                Status = instance.Ack.ToString(),
                PayerId = instance.GetExpressCheckoutDetailsResponseDetails.PayerInfo.PayerID
            };
        }
    }
}