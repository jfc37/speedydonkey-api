using Models.OnlinePayments;

namespace OnlinePayments.PaymentMethods.Poli.Models
{
    public class GetTransactionResponse : IAddError
    {
        public string TransactionRefNo { get; set; }
        public string CurrencyCode { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public string TransactionStatusCode { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public void AddError(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}