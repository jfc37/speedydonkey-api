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
    public class DeletePersonHandlerTestFixture
    {
        private DeletePersonHandlerBuilder _deletePersonHandlerBuilder;
        private MockPersonRepositoryBuilder<Student> _personRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _deletePersonHandlerBuilder = new DeletePersonHandlerBuilder();
            _personRepositoryBuilder = new MockPersonRepositoryBuilder<Student>()
                .WithSuccessfulDelete();
        }

        private DeletePersonHandler BuildDeletePersonHandler()
        {
            return _deletePersonHandlerBuilder
                .WithPersonRepository(_personRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : DeletePersonHandlerTestFixture
        {
            private DeletePerson _deletePerson;

            [SetUp]
            public void Setup()
            {
                _deletePerson = new DeletePerson(new PersonBuilder().BuildStudent());
            }

            [Test]
            public void It_should_call_delete_on_person_repository()
            {
                var deletePersonHanlder = BuildDeletePersonHandler();
                deletePersonHanlder.Handle(_deletePerson);

                _personRepositoryBuilder.Mock.Verify(x => x.Delete(_deletePerson.ActionAgainst));
            }
        }
    }
}
