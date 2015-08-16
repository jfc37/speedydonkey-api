using NUnit.Framework;
using OnlinePayments.PaymentMethods.Poli;

namespace OnlinePaymentsTests.Calculations
{
    [TestFixture]
    public class PoliFeeCalculationTests
    {
        [TestCase(100, 1)]
        [TestCase(199, 1.99)]
        public void It_should_return_the_correct_value(decimal amount, decimal expected)
        {
            var result = new PoliFeeCalculation(amount)
                .Calculate()
                .Result();

            Assert.AreEqual(expected, result);
        }
    }
}
