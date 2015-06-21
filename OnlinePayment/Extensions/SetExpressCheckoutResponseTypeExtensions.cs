using System.Linq;
using OnlinePayment.Models;
using PayPal.PayPalAPIInterfaceService.Model;

namespace OnlinePayment.Extensions
{
    public static class SetExpressCheckoutResponseTypeExtensions
    {
        public static SetExpressCheckoutResponse ToResponse(this SetExpressCheckoutResponseType instance)
        {
            return new SetExpressCheckoutResponse
            {
                Token = instance.Token,
                Status = instance.Ack.GetValueOrDefault().ToString(),
                Errors = instance.Errors.Select(x => ErrorTypeExtensions.ToPaypalError(x))
            };
        }
    }
}