using OnlinePayments.Models;
using PayPal.PayPalAPIInterfaceService.Model;

namespace OnlinePayments.Extensions
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