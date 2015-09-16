using System.Linq;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;

namespace IntegrationTests.Utilities.ModelVerfication
{
    public class VerifyLevelProperties
    {
        private readonly LevelModel _expected;
        private readonly LevelModel _actual;

        public VerifyLevelProperties(LevelModel expected, LevelModel actual)
        {
            _expected = expected;
            _actual = actual;
        }

        public void Verify()
        {
            Assert.AreEqual(_expected.Name, _actual.Name);
            Assert.AreEqual(_expected.StartTime, _actual.StartTime);
            Assert.AreEqual(_expected.ClassMinutes, _actual.ClassMinutes);
            Assert.AreEqual(_expected.ClassesInBlock, _actual.ClassesInBlock);
            Assert.AreEqual(_expected.EndTime, _actual.EndTime);
            Assert.AreEqual(_expected.Teachers.Select(x => x.Id), _actual.Teachers.Select(x => x.Id));

            _actual.Teachers.ForEach(x =>
            {
                Assert.IsNotNullOrEmpty(x.FirstName);
                Assert.IsNotNullOrEmpty(x.Surname);
            });
        }
    }
}