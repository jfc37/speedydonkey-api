using NUnit.Framework;
using OnlinePayments.PaymentMethods.PayPal;

namespace OnlinePaymentsTests.Calculations
{
    [TestFixture]
    public class PayPalFeeCalculationTests
    {
        [TestCase(100, 3.85)]
        [TestCase(199, 7.216)]
        public void It_should_return_the_correct_value(decimal amount, decimal expected)
        {
            var result = new PayPalFeeCalculation(amount)
                .Calculate()
                .Result();

            Assert.AreEqual(expected, result);
        }
    }
}
