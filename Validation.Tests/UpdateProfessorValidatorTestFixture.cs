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
    public class UpdateProfessorValidatorTestFixture
    {
        private UpdateProfessorValidatorBuilder _updateProfessorValidatorBuilder;
        private MockPersonRepositoryBuilder<Professor> _professorRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _updateProfessorValidatorBuilder = new UpdateProfessorValidatorBuilder();
            _professorRepositoryBuilder = new MockPersonRepositoryBuilder<Professor>();
        }

        private UpdateProfessorValidator BuildUpdateProfessorValidator()
        {
            return _updateProfessorValidatorBuilder
                .WithProfessorRepository(_professorRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Validate : UpdateProfessorValidatorTestFixture
        {
            private PersonBuilder _personBuilder;

            [SetUp]
            public void Setup()
            {
                _personBuilder = new PersonBuilder()
                    .WithValidInputs();
                _professorRepositoryBuilder
                    .WithNoPeople()
                    .WithPerson(_personBuilder.BuildProfessor());
            }

            [Test]
            public void It_should_return_no_errors_when_professor_is_valid()
            {
                _personBuilder = new PersonBuilder()
                    .WithValidInputs();
                _professorRepositoryBuilder
                    .WithNoPeople()
                    .WithPerson(_personBuilder.BuildProfessor());

                var updateProfessorValidator = BuildUpdateProfessorValidator();
                var results = updateProfessorValidator.Validate(_personBuilder.BuildProfessor());

                Assert.IsTrue(results.IsValid);
            }

            [Test]
            public void It_should_require_professor_be_existing()
            {
                _personBuilder.WithId(1);
                _professorRepositoryBuilder
                    .WithNoPeople();

                var updateProfessorValidator = BuildUpdateProfessorValidator();
                var results = updateProfessorValidator.Validate(_personBuilder.BuildProfessor());

                Assert.AreEqual("Id", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.ProfessorDoesntExist, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_first_name()
            {
                _personBuilder.WithFirstName(null);

                var updateProfessorValidator = BuildUpdateProfessorValidator();
                var results = updateProfessorValidator.Validate(_personBuilder.BuildProfessor());

                Assert.AreEqual("FirstName", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingFirstName, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_first_name_not_be_empty()
            {
                _personBuilder.WithFirstName("");

                var updateProfessorValidator = BuildUpdateProfessorValidator();
                var results = updateProfessorValidator.Validate(_personBuilder.BuildProfessor());

                Assert.AreEqual("FirstName", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingFirstName, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_surname()
            {
                _personBuilder.WithSurname(null);

                var updateProfessorValidator = BuildUpdateProfessorValidator();
                var results = updateProfessorValidator.Validate(_personBuilder.BuildProfessor());

                Assert.AreEqual("Surname", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingSurname, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_surname_not_be_empty()
            {
                _personBuilder.WithSurname("");

                var updateProfessorValidator = BuildUpdateProfessorValidator();
                var results = updateProfessorValidator.Validate(_personBuilder.BuildProfessor());

                Assert.AreEqual("Surname", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingSurname, results.Errors.Single().ErrorMessage);
            }
        }
    }

    public class UpdateProfessorValidatorBuilder
    {
        private IPersonRepository<Professor> _professorRepository;

        public UpdateProfessorValidator Build()
        {
            return new UpdateProfessorValidator(_professorRepository);
        }

        public UpdateProfessorValidatorBuilder WithProfessorRepository(IPersonRepository<Professor> professorRepository)
        {
            _professorRepository = professorRepository;
            return this;
        }
    }
}
