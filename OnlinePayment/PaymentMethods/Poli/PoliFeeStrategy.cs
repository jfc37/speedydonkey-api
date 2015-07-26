using Models.OnlinePayments;
using OnlinePayments.PaymentFeeStrategies;

namespace OnlinePayments.PaymentMethods.Poli
{
    public class PoliFeeStrategy : IPaymentFeeStrategy
    {
        public decimal GetFee(OnlinePayment onlinePayment)
        {
            return new PoliFeeCalculation(onlinePayment.Price)
                .Calculate()
                .Result();
        }
    }
}
