using Common.Extensions;
using NUnit.Framework;

namespace Common.Tests.Extensions.Decimals
{
    [TestFixture]
    public class ToCurrencyStringTests
    {
        [TestCase(1.1, "1.10")]
        [TestCase(1, "1.00")]
        [TestCase(1.12, "1.12")]
        [TestCase(1.125, "1.13")]
        [TestCase(1.124, "1.12")]
        [TestCase(12.1, "12.10")]
        public void It_should_convert_decimal_to_2dp(decimal value, string expected)
        {
            Assert.AreEqual(expected, value.ToCurrencyString());
        }
    }
}
