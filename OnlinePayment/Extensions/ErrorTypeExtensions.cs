using OnlinePayment.Models;
using PayPal.PayPalAPIInterfaceService.Model;

namespace OnlinePayment.Extensions
{
    public static class ErrorTypeExtensions
    {
        public static PaypalError ToPaypalError(this ErrorType instance)
        {
            return new PaypalError
            {
                ErrorCode = instance.ErrorCode,
                LongMessage = instance.LongMessage,
                SeverityCode = instance.SeverityCode.ToString(),
                ShortMessage = instance.ShortMessage
            };
        }
    }
}