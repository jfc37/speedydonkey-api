using System.Collections.Generic;
using Common.Extensions;
using Models.OnlinePayments;

namespace OnlinePayments.PaymentMethods.Poli.Models
{
    public class PoliCompleteResponse : ICompleteOnlinePaymentResponse
    {
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
