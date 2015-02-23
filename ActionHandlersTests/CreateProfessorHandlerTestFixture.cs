using ActionHandlers;
using ActionHandlersTests.Builders;
using Actions;
using Data.Tests.Builders.MockBuilders;
using Models;
using Models.Tests.Builders;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class CreateProfessorHandlerTestFixture
    {
        private CreatePersonHandlerBuilder _createPersonHandlerBuilder;
        private MockPersonRepositoryBuilder<Professor> _personRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _createPersonHandlerBuilder = new CreatePersonHandlerBuilder();
            _personRepositoryBuilder = new MockPersonRepositoryBuilder<Professor>()
                .WithSuccessfulCreation();
        }

        private CreateProfessorHandler BuildCreatePersonHandler()
        {
            return _createPersonHandlerBuilder
                .WithPersonRepository(_personRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : CreateProfessorHandlerTestFixture
        {
            private CreatePerson _createPerson;

            [SetUp]
            public void Setup()
            {
                _createPerson = new CreatePerson(new PersonBuilder().BuildProfessor());
            }

            [Test]
            public void It_should_call_create_on_person_repository()
            {
                var createPersonHanlder = BuildCreatePersonHandler();
                createPersonHanlder.Handle(_createPerson);

                _personRepositoryBuilder.Mock.Verify(x => x.Create(_createPerson.ActionAgainst));
            }
        }
    }
}
