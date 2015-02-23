using ActionHandlers;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class PasswordHasherTestFixture
    {
        private PasswordHasher _passwordHasher;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _passwordHasher = new PasswordHasher();
        }

        public class CreateHash : PasswordHasherTestFixture
        {
            private string _password;

            [SetUp]
            public void Setup()
            {
                _password = "password";
            }

            [Test]
            public void It_should_return_hash_that_is_different_from_password()
            {
                string hash = _passwordHasher.CreateHash(_password);
                Assert.AreNotEqual(_password, hash.Length);
            }
        }

        public class ValidatePassword : PasswordHasherTestFixture
        {
            private string _password;
            private string _hashedPassword;

            [SetUp]
            public void Setup()
            {
                _password = "password";
                _hashedPassword = _passwordHasher.CreateHash(_password);
            }

            [Test]
            public void It_should_return_true_when_password_is_correct()
            {
                Assert.IsTrue(_passwordHasher.ValidatePassword(_password, _hashedPassword));
            }

            [Test]
            public void It_should_return_false_when_password_is_incorrect()
            {
                Assert.IsFalse(_passwordHasher.ValidatePassword("password1", _hashedPassword));
            }
        }
    }
}
