using System;
using System.Linq;

namespace Models.OnlinePayments
{
    public interface IStartOnlinePaymentResponse
    {
        bool IsValid { get; }
        Guid ReferenceNumber { get; set; }

        void AddError(string errorMessage);
    }
}