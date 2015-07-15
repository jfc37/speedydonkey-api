using ActionHandlers.Calculations;
using NUnit.Framework;

namespace ActionHandlersTests.Calculations
{
    [TestFixture]
    public class ExpressCheckoutTotalCalculationTests
    {
        [TestCase(100, 103.85)]
        public void It_should_return_the_correct_value(decimal amount, decimal expected)
        {
            var result = new ExpressCheckoutTotalCalculation(amount)
                .Calculate()
                .Result();

            Assert.AreEqual(expected, result);
        }
    }
}
