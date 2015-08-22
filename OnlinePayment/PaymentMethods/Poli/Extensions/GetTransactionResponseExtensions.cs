using Common.Extensions;
using OnlinePayments.PaymentMethods.Poli.Models;

namespace OnlinePayments.PaymentMethods.Poli.Extensions
{
    public static class GetTransactionResponseExtensions
    {
        public static PoliCompleteResponse ToPoliCompleteResponse(this GetTransactionResponse instance)
        {
            var response = new PoliCompleteResponse
            {
                Status = instance.TransactionStatusCode
            };

            if (instance.ErrorMessage.IsNotNullOrWhiteSpace())
                response.AddError(instance.ErrorMessage);
            if (instance.AmountPaid.NotEquals(instance.PaymentAmount))
                response.AddError("Expected {0} to be paid, but was actually {1}".FormatWith(instance.PaymentAmount, instance.AmountPaid));

            return response;
        }
    }
}