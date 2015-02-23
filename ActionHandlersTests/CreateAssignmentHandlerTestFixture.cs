using ActionHandlers;
using Actions;
using Data.Repositories;
using Data.Tests.Builders.MockBuilders;
using Models.Tests.Builders;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class CreateAssignmentHandlerTestFixture
    {
        private CreateAssignmentHandlerBuilder _createAssignmentHandlerBuilder;
        private MockAssignmentRepositoryBuilder _assignmentRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _createAssignmentHandlerBuilder = new CreateAssignmentHandlerBuilder();
            _assignmentRepositoryBuilder = new MockAssignmentRepositoryBuilder()
                .WithSuccessfulCreation();
        }

        private CreateAssignmentHandler BuildCreateAssignmentHandler()
        {
            return _createAssignmentHandlerBuilder
                .WithAssignmentRepository(_assignmentRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : CreateAssignmentHandlerTestFixture
        {
            private CreateAssignment _createAssignment;

            [SetUp]
            public void Setup()
            {
                _createAssignment = new CreateAssignment(new AssignmentBuilder().Build());
            }

            [Test]
            public void It_should_call_create_on_assignment_repository()
            {
                var createAssignmentHanlder = BuildCreateAssignmentHandler();
                createAssignmentHanlder.Handle(_createAssignment);

                _assignmentRepositoryBuilder.Mock.Verify(x => x.Create(_createAssignment.ActionAgainst));
            }
        }
    }

    public class CreateAssignmentHandlerBuilder
    {
        private IAssignmentRepository _assignmentRepository;

        public CreateAssignmentHandler Build()
        {
            return new CreateAssignmentHandler(_assignmentRepository);
        }

        public CreateAssignmentHandlerBuilder WithAssignmentRepository(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
            return this;
        }
    }
}
