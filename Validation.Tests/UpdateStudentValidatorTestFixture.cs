using System.Linq;
using Data.Repositories;
using Data.Tests.Builders.MockBuilders;
using Models;
using Models.Tests.Builders;
using NUnit.Framework;
using Validation.Validators;

namespace Validation.Tests
{
    [TestFixture]
    public class UpdateStudentValidatorTestFixture
    {
        private UpdateStudentValidatorBuilder _updateStudentValidatorBuilder;
        private MockPersonRepositoryBuilder<Student> _studentRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _updateStudentValidatorBuilder = new UpdateStudentValidatorBuilder();
            _studentRepositoryBuilder = new MockPersonRepositoryBuilder<Student>();
        }

        private UpdateStudentValidator BuildUpdateStudentValidator()
        {
            return _updateStudentValidatorBuilder
                .WithStudentRepository(_studentRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Validate : UpdateStudentValidatorTestFixture
        {
            private PersonBuilder _personBuilder;

            [SetUp]
            public void Setup()
            {
                _personBuilder = new PersonBuilder()
                    .WithValidInputs();
                _studentRepositoryBuilder
                    .WithNoPeople()
                    .WithPerson(_personBuilder.BuildStudent());
            }

            [Test]
            public void It_should_return_no_errors_when_student_is_valid()
            {
                _personBuilder = new PersonBuilder()
                    .WithValidInputs();
                _studentRepositoryBuilder
                    .WithNoPeople()
                    .WithPerson(_personBuilder.BuildStudent());

                var updateStudentValidator = BuildUpdateStudentValidator();
                var results = updateStudentValidator.Validate(_personBuilder.BuildStudent());

                Assert.IsTrue(results.IsValid);
            }

            [Test]
            public void It_should_require_student_be_existing()
            {
                _personBuilder.WithId(1);
                _studentRepositoryBuilder
                    .WithNoPeople();

                var updateStudentValidator = BuildUpdateStudentValidator();
                var results = updateStudentValidator.Validate(_personBuilder.BuildStudent());

                Assert.AreEqual("Id", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.StudentDoesntExist, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_first_name()
            {
                _personBuilder.WithFirstName(null);

                var updateStudentValidator = BuildUpdateStudentValidator();
                var results = updateStudentValidator.Validate(_personBuilder.BuildStudent());

                Assert.AreEqual("FirstName", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingFirstName, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_first_name_not_be_empty()
            {
                _personBuilder.WithFirstName("");

                var updateStudentValidator = BuildUpdateStudentValidator();
                var results = updateStudentValidator.Validate(_personBuilder.BuildStudent());

                Assert.AreEqual("FirstName", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingFirstName, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_surname()
            {
                _personBuilder.WithSurname(null);

                var updateStudentValidator = BuildUpdateStudentValidator();
                var results = updateStudentValidator.Validate(_personBuilder.BuildStudent());

                Assert.AreEqual("Surname", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingSurname, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_surname_not_be_empty()
            {
                _personBuilder.WithSurname("");

                var updateStudentValidator = BuildUpdateStudentValidator();
                var results = updateStudentValidator.Validate(_personBuilder.BuildStudent());

                Assert.AreEqual("Surname", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingSurname, results.Errors.Single().ErrorMessage);
            }
        }
    }

    public class UpdateStudentValidatorBuilder
    {
        private IPersonRepository<Student> _studentRepository;

        public UpdateStudentValidator Build()
        {
            return new UpdateStudentValidator(_studentRepository);
        }

        public UpdateStudentValidatorBuilder WithStudentRepository(IPersonRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
            return this;
        }
    }
}
