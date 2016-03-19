using Contracts.Passes;
using NUnit.Framework;

namespace IntegrationTests.Utilities.ModelVerfication
{
    public static class VerifyClipPassProperties
    {
        public static void Verify(PassModel expected, PassModel actual)
        {
            Assert.AreEqual(expected.StartDate, actual.StartDate);
            Assert.AreEqual(expected.EndDate, actual.EndDate);
        }
    }
}