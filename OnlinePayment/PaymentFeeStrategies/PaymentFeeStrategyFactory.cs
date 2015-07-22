using System;
using Common;
using Models.OnlinePayments;

namespace OnlinePayments.PaymentFeeStrategies
{
    public class PaymentFeeStrategyFactory : IPaymentFeeStrategyFactory
    {
        public IPaymentFeeStrategy GetPaymentFeeStrategy(PaymentMethod paymentMethod)
        {
            switch (paymentMethod)
            {
                case PaymentMethod.PayPal:
                    return new PayPalFeeStrategy();
                case PaymentMethod.BankDeposit:
                    return new NoFeeStrategy();
                default:
                    throw new ArgumentException("Don't have strategy for payment method: {0}".FormatWith(paymentMethod));
            }
        }
    }
}