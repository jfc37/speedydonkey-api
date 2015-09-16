using NUnit.Framework;
using SpeedyDonkeyApi.Models;

namespace IntegrationTests.Utilities.ModelVerfication
{
    public class VerifyUserProperties
    {
        private readonly UserModel _expected;
        private readonly UserModel _actual;

        public VerifyUserProperties(UserModel expected, UserModel actual)
        {
            _expected = expected;
            _actual = actual;
        }

        public void Verify()
        {
            Assert.AreEqual(_expected.Email, _actual.Email);
            Assert.AreEqual(_expected.FirstName, _actual.FirstName);
            Assert.AreEqual(_expected.Surname, _actual.Surname);

            Assert.IsNullOrEmpty(_actual.Password);
        }
    }
}