using Contracts.Events;
using Contracts.Rooms;
using NUnit.Framework;

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
    public class VerifyStandAloneEventProperties
    {
        private readonly StandAloneEventModel _expected;
        private readonly StandAloneEventModel _actual;

        public VerifyStandAloneEventProperties(StandAloneEventModel expected, StandAloneEventModel actual)
        {
            _expected = expected;
            _actual = actual;
        }

        public void Verify()
        {
            Assert.AreEqual(_expected.Name, _actual.Name);
            Assert.AreEqual(_expected.IsPrivate, _actual.IsPrivate);
            Assert.AreEqual(_expected.ClassCapacity, _actual.ClassCapacity);
            Assert.AreEqual(_expected.EndTime, _actual.EndTime);
            Assert.AreEqual(_expected.StartTime, _actual.StartTime);
            Assert.AreEqual(_expected.TeacherRate, _actual.TeacherRate);
        }
    }
}