using System;

namespace Models.OnlinePayments
{
    public interface IStartOnlinePaymentResponse
    {
        bool IsValid { get; }
        Guid ReferenceNumber { get; set; }

        void AddError(string errorMessage);
    }

    public interface ICompleteOnlinePaymentResponse
    {
        bool IsValid { get; }

        void AddError(string errorMessage);
    }
}