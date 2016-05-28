using Contracts.Teachers;
using NUnit.Framework;

namespace IntegrationTests.Utilities.ModelVerfication
{
    public class VerifyTeacherRateProperties
    {
        private readonly TeacherRateModel _expected;
        private readonly TeacherRateModel _actual;

        public VerifyTeacherRateProperties(TeacherRateModel expected, TeacherRateModel actual)
        {
            _expected = expected;
            _actual = actual;
        }

        public void Verify()
        {
            Assert.AreEqual(_expected.PartnerRate, _actual.PartnerRate);
            Assert.AreEqual(_expected.SoloRate, _actual.SoloRate);
        }
    }
}