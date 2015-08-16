using ActionHandlers.Calculations;
using NUnit.Framework;

namespace ActionHandlersTests.Calculations
{
    [TestFixture]
    public class ExpressCheckoutFeeCalculationTests
    {
        [TestCase(100, 3.85)]
        public void It_should_return_the_correct_value(decimal amount, decimal expected)
        {
            var result = new ExpressCheckoutFeeCalculation(amount)
                .Calculate()
                .Result();

            Assert.AreEqual(expected, result);
        }
    }
}
