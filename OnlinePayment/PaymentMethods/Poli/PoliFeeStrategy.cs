using Models.OnlinePayments;
using OnlinePayments.PaymentFeeStrategies;

namespace OnlinePayments.PaymentMethods.Poli
{
    public class PoliFeeStrategy : IPaymentFeeStrategy
    {
        public decimal GetFee(OnlinePayment onlinePayment)
        {
            if (onlinePayment.ItemType == OnlinePaymentItem.WindyLindy)
            {
                return new NoFeeStrategy()
                    .GetFee(onlinePayment);
            }

            return new PoliFeeCalculation(onlinePayment.Price)
                .Calculate()
                .Result();
        }
    }
}
