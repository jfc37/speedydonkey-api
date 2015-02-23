using ActionHandlers;
using Actions;
using Data.Repositories;
using Data.Tests.Builders.MockBuilders;
using Models;
using Models.Tests.Builders;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class UpdateProfessorHandlerTestFixture
    {
        private UpdateProfessorHandlerBuilder _updateProfessorHandlerBuilder;
        private MockPersonRepositoryBuilder<Professor> _professorRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _updateProfessorHandlerBuilder = new UpdateProfessorHandlerBuilder();
            _professorRepositoryBuilder = new MockPersonRepositoryBuilder<Professor>()
                .WithSuccessfulUpdate();
        }

        private UpdateProfessorHandler BuildUpdateProfessorHandler()
        {
            return _updateProfessorHandlerBuilder
                .WithProfessorRepository(_professorRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : UpdateProfessorHandlerTestFixture
        {
            private UpdateProfessor _createProfessor;

            [SetUp]
            public void Setup()
            {
                _createProfessor = new UpdateProfessor(new PersonBuilder().BuildProfessor());
                _professorRepositoryBuilder.WithPerson(_createProfessor.ActionAgainst);
            }

            [Test]
            public void It_should_call_update_on_professor_repository()
            {
                var createProfessorHanlder = BuildUpdateProfessorHandler();
                createProfessorHanlder.Handle(_createProfessor);

                _professorRepositoryBuilder.Mock.Verify(x => x.Update(_createProfessor.ActionAgainst));
            }
        }
    }

    public class UpdateProfessorHandlerBuilder
    {
        private IPersonRepository<Professor> _professorRepository;

        public UpdateProfessorHandler Build()
        {
            return new UpdateProfessorHandler(_professorRepository);
        }

        public UpdateProfessorHandlerBuilder WithProfessorRepository(IPersonRepository<Professor> professorRepository)
        {
            _professorRepository = professorRepository;
            return this;
        }
    }
}
