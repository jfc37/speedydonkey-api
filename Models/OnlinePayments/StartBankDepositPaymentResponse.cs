using System.Collections.Generic;
using Common;

namespace Models.OnlinePayments
{
    public class StartBankDepositPaymentResponse : IStartOnlinePaymentResponse
    {
        public List<string> Errors { get; set; } 
        public bool IsValid { get { return Errors.NotAny(); } }

        public StartBankDepositPaymentResponse()
        {
            Errors = new List<string>();
        }

        public void AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
        }
    }
}