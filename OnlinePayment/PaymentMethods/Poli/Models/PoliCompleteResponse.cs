using System.Collections.Generic;
using Common.Extensions;
using Models.OnlinePayments;

namespace OnlinePayments.PaymentMethods.Poli.Models
{
    public class PoliCompleteResponse : ICompleteOnlinePaymentResponse
    {
        public PoliCompleteResponse(dynamic latest)
        {
            Errors = new List<string>();
            Status = latest.TransactionStatusCode;

            if (latest.ErrorMessage.HasValues)
                Errors.Add(latest.ErrorMessage);

            //if (Status.IsNotSameAs("Completed"))
            //    Errors.Add("Transaction not completed");

            //if (latest.PaymentAmount.NotEquals(latest.AmountPaid))
            //    Errors.Add("Different amount paid");

        }

        public PoliCompleteResponse()
        {
            Errors = new List<string>();
        }

        public string Status { get; set; }
        public List<string> Errors { get; set; }
        public bool IsValid { get { return Errors.NotAny(); } }
        public void AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
        }
    }
}
