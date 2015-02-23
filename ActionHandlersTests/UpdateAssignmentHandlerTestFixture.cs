using ActionHandlers;
using Actions;
using Data.Repositories;
using Data.Tests.Builders.MockBuilders;
using Models.Tests.Builders;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class UpdateAssignmentHandlerTestFixture
    {
        private UpdateAssignmentHandlerBuilder _createAssignmentHandlerBuilder;
        private MockAssignmentRepositoryBuilder _assignmentRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _createAssignmentHandlerBuilder = new UpdateAssignmentHandlerBuilder();
            _assignmentRepositoryBuilder = new MockAssignmentRepositoryBuilder()
                .WithSuccessfulUpdate();
        }

        private UpdateAssignmentHandler BuildUpdateAssignmentHandler()
        {
            return _createAssignmentHandlerBuilder
                .WithAssignmentRepository(_assignmentRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : UpdateAssignmentHandlerTestFixture
        {
            private UpdateAssignment _createAssignment;

            [SetUp]
            public void Setup()
            {
                _createAssignment = new UpdateAssignment(new AssignmentBuilder().Build());
                _assignmentRepositoryBuilder.WithAssignment(_createAssignment.ActionAgainst);
            }

            [Test]
            public void It_should_call_update_on_assignment_repository()
            {
                var createAssignmentHanlder = BuildUpdateAssignmentHandler();
                createAssignmentHanlder.Handle(_createAssignment);

                _assignmentRepositoryBuilder.Mock.Verify(x => x.Update(_createAssignment.ActionAgainst));
            }
        }
    }

    public class UpdateAssignmentHandlerBuilder
    {
        private IAssignmentRepository _assignmentRepository;

        public UpdateAssignmentHandler Build()
        {
            return new UpdateAssignmentHandler(_assignmentRepository);
        }

        public UpdateAssignmentHandlerBuilder WithAssignmentRepository(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
            return this;
        }
    }
}
