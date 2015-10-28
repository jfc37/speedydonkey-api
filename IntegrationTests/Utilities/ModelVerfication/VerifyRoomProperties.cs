using NUnit.Framework;
using SpeedyDonkeyApi.Models;

namespace IntegrationTests.Utilities.ModelVerfication
{
    public class VerifyRoomProperties
    {
        private readonly RoomModel _expected;
        private readonly RoomModel _actual;

        public VerifyRoomProperties(RoomModel expected, RoomModel actual)
        {
            _expected = expected;
            _actual = actual;
        }

        public void Verify()
        {
            Assert.AreEqual(_expected.Name, _actual.Name);
            Assert.AreEqual(_expected.Location, _actual.Location);
        }
    }
}