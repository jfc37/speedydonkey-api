using Models.OnlinePayments;

namespace OnlinePayments.PaymentMethods.Poli.Models
{
    public class InitiatePoliTransaction : IAddError
    {
        public bool Success { get; set; }
        public string TransactionRefNo { get; set; }
        public string NavigateURL { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public void AddError(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}