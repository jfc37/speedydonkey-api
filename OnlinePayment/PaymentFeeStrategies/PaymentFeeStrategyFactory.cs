using System;
using Common;
using Common.Extensions;
using Models.OnlinePayments;
using OnlinePayments.PaymentMethods.PayPal;
using OnlinePayments.PaymentMethods.Poli;

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
                case PaymentMethod.Poli:
                    return new PoliFeeStrategy();
                default:
                    throw new ArgumentException("Don't have strategy for payment method: {0}".FormatWith(paymentMethod));
            }
        }
    }
}