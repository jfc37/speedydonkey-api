using Contracts.MappingExtensions;
using Contracts.PrivateLessons;
using NUnit.Framework;

namespace IntegrationTests.Utilities.ModelVerfication
{
    public class VerifyOpeningHoursProperties
    {
        private readonly TimeSlotModel _expected;
        private readonly TimeSlotModel _actual;

        public VerifyOpeningHoursProperties(TimeSlotModel expected, TimeSlotModel actual)
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