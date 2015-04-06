using Data.Tests.Builders;
using Models;
using NUnit.Framework;
using Validation.Validators;

namespace Validation.Tests
{
    [TestFixture]
    public class ForgottenPasswordValidatorTests
    {
        private MockRepositoryBuilder<User> RepositoryBuilder;
        private User User;
        private string _email;

        [SetUp]
        public void Setup()
        {
            _email = "blah";
            RepositoryBuilder = new MockRepositoryBuilder<User>()
                .WithGet(new User
                {
                    Email = _email,
                    Status = UserStatus.Active
                });
            User = new User
            {
                Email =  _email
            };
        }

        private ForgottenPasswordValidator GetValidator()
        {
            return new ForgottenPasswordValidator(RepositoryBuilder.BuildObject());
        }

        private FluentValidation.Results.ValidationResult PerformAction()
        {
            return GetValidator().Validate(User);
        }

        [Test]
        public void When_everything_is_fine_it_should_return_no_errors()
        {
            var result = PerformAction();

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void When_email_doesnt_match_it_should_return_an_error()
        {
            RepositoryBuilder.WithUnsuccessfulGet();

            var result = PerformAction();

            Assert.IsFalse(result.IsValid);
        }
    }
}
