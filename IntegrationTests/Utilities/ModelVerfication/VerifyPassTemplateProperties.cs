using Contracts.Passes;
using NUnit.Framework;

namespace IntegrationTests.Utilities.ModelVerfication
{
    public class VerifyPassTemplateProperties
    {
        private readonly PassTemplateModel _expected;
        private readonly PassTemplateModel _actual;

        public VerifyPassTemplateProperties(PassTemplateModel expected, PassTemplateModel actual)
        {
            _expected = expected;
            _actual = actual;
        }

        public void Verify()
        {
            Assert.AreEqual(_expected.AvailableForPurchase, _actual.AvailableForPurchase);
            Assert.AreEqual(_expected.ClassesValidFor, _actual.ClassesValidFor);
            Assert.AreEqual(_expected.Cost, _actual.Cost);
            Assert.AreEqual(_expected.Description, _actual.Description);
            Assert.AreEqual(_expected.PassType, _actual.PassType);
            Assert.AreEqual(_expected.WeeksValidFor, _actual.WeeksValidFor);
        }
    }
}