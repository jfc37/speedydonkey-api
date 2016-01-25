using Contracts.MappingExtensions;
using Contracts.PrivateLessons;
using NUnit.Framework;

namespace IntegrationTests.Utilities.ModelVerfication
{
    public class VerifyOpeningHoursProperties
    {
        private readonly OpeningHoursModel _expected;
        private readonly OpeningHoursModel _actual;

        public VerifyOpeningHoursProperties(OpeningHoursModel expected, OpeningHoursModel actual)
        {
            _expected = expected;
            _actual = actual;
        }

        public void Verify()
        {
            Assert.AreEqual(_expected.Day, _actual.Day);
            Assert.AreEqual(_expected.ClosingTime.ToEntity(), _actual.ClosingTime.ToEntity());
            Assert.AreEqual(_expected.OpeningTime.ToEntity(), _actual.OpeningTime.ToEntity());
        }
    }
}