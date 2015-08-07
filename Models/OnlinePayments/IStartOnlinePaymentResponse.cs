using System;

namespace Models.OnlinePayments
{
    public interface IAddError
    {

        void AddError(string errorMessage);   
    }

    public interface IStartOnlinePaymentResponse : IAddError
    {
        bool IsValid { get; }
        Guid ReferenceNumber { get; set; }
    }

    public interface ICompleteOnlinePaymentResponse : IAddError
    {
        bool IsValid { get; }
    }
}