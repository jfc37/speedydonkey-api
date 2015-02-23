using System.Linq;
using Data;
using Data.Tests.Builders.MockBuilders;
using Models;
using Models.Tests.Builders;
using NUnit.Framework;
using Validation.Validators;

namespace Validation.Tests
{
    [TestFixture]
    public class CreatePersonValidatorTestFixture
    {
        private CreatePersonValidatorBuilder _createPersonValidatorBuilder;
        private MockDbContextBuilder _contextBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _createPersonValidatorBuilder = new CreatePersonValidatorBuilder();
            _contextBuilder = new MockDbContextBuilder();
        }

        private CreatePersonValidator BuildCreatePersonValidator()
        {
            return _createPersonValidatorBuilder
                .WithContext(_contextBuilder.BuildObject())
                .Build();
        }

        public class Validate : CreatePersonValidatorTestFixture
        {
            private PersonBuilder _personBuilder;

            [SetUp]
            public void Setup()
            {
                var existingUser = new User {Id = 643};

                _personBuilder = new PersonBuilder()
                    .WithValidInputs()
                    .WithUser(existingUser);
                _contextBuilder.WithUser(existingUser);
            }

            [Test]
            public void It_should_return_no_errors_when_person_is_valid()
            {
                var existingUser = new User { Id = 643 };
                _personBuilder = new PersonBuilder()
                    .WithValidInputs()
                    .WithUser(existingUser);
                _contextBuilder.WithUser(existingUser);

                var createPersonValidator = BuildCreatePersonValidator();
                var results = createPersonValidator.Validate(_personBuilder.BuildStudent());

                Assert.IsTrue(results.IsValid);
            }

            [Test]
            public void It_should_require_first_name()
            {
                _personBuilder.WithFirstName(null);

                var createPersonValidator = BuildCreatePersonValidator();
                var results = createPersonValidator.Validate(_personBuilder.BuildStudent());

                Assert.AreEqual("FirstName", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingFirstName, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_first_name_not_be_empty()
            {
                _personBuilder.WithFirstName("");

                var createPersonValidator = BuildCreatePersonValidator();
                var results = createPersonValidator.Validate(_personBuilder.BuildStudent());

                Assert.AreEqual("FirstName", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingFirstName, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_surname()
            {
                _personBuilder.WithSurname(null);

                var createPersonValidator = BuildCreatePersonValidator();
                var results = createPersonValidator.Validate(_personBuilder.BuildStudent());

                Assert.AreEqual("Surname", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingSurname, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_surname_not_be_empty()
            {
                _personBuilder.WithSurname("");

                var createPersonValidator = BuildCreatePersonValidator();
                var results = createPersonValidator.Validate(_personBuilder.BuildStudent());

                Assert.AreEqual("Surname", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingSurname, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_linked_to_a_user()
            {
                _personBuilder.WithUser(null);

                var createPersonValidator = BuildCreatePersonValidator();
                var results = createPersonValidator.Validate(_personBuilder.BuildStudent());

                Assert.AreEqual("User", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.NoLinkedUser, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_linked_user_exists()
            {
                _contextBuilder.WithNoUsers();
                _personBuilder.WithUser(new User{Id = 44});

                var createPersonValidator = BuildCreatePersonValidator();
                var results = createPersonValidator.Validate(_personBuilder.BuildStudent());

                Assert.AreEqual("User", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.UserDoesntExist, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_linked_user_not_to_have_person_already()
            {
                var existingUser =
                    new UserBuilder().WithId(33).WithPerson(new PersonBuilder().WithUser(new User {Id = 33}).BuildStudent()).Build();
                _contextBuilder.WithUser(existingUser);
                _personBuilder.WithUser(new User{Id = 33});

                var createPersonValidator = BuildCreatePersonValidator();
                var results = createPersonValidator.Validate(_personBuilder.BuildStudent());

                Assert.AreEqual("User", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.LinkedUserAlreadyHasPerson, results.Errors.Single().ErrorMessage);
            }
        }
    }

    public class CreatePersonValidatorBuilder
    {
        private ISpeedyDonkeyDbContext _context;

        public CreatePersonValidatorBuilder WithContext(ISpeedyDonkeyDbContext context)
        {
            _context = context;
            return this;
        }

        public CreatePersonValidator Build()
        {
            return new CreatePersonValidator(_context);
        }
    }
}
