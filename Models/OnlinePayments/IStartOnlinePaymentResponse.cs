using System.Linq;

namespace Models.OnlinePayments
{
    public interface IStartOnlinePaymentResponse
    {
        bool IsValid { get; }

        void AddError(string errorMessage);
    }
}