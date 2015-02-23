using ActionHandlers;
using Actions;
using Data.Repositories;
using Data.Tests.Builders.MockBuilders;
using Models.Tests.Builders;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class CreateExamHandlerTestFixture
    {
        private CreateExamHandlerBuilder _createExamHandlerBuilder;
        private MockExamRepositoryBuilder _examRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _createExamHandlerBuilder = new CreateExamHandlerBuilder();
            _examRepositoryBuilder = new MockExamRepositoryBuilder()
                .WithSuccessfulCreation();
        }

        private CreateExamHandler BuildCreateExamHandler()
        {
            return _createExamHandlerBuilder
                .WithExamRepository(_examRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : CreateExamHandlerTestFixture
        {
            private CreateExam _createExam;

            [SetUp]
            public void Setup()
            {
                _createExam = new CreateExam(new ExamBuilder().Build());
            }

            [Test]
            public void It_should_call_create_on_exam_repository()
            {
                var createExamHanlder = BuildCreateExamHandler();
                createExamHanlder.Handle(_createExam);

                _examRepositoryBuilder.Mock.Verify(x => x.Create(_createExam.ActionAgainst));
            }
        }
    }

    public class CreateExamHandlerBuilder
    {
        private IExamRepository _examRepository;

        public CreateExamHandler Build()
        {
            return new CreateExamHandler(_examRepository);
        }

        public CreateExamHandlerBuilder WithExamRepository(IExamRepository examRepository)
        {
            _examRepository = examRepository;
            return this;
        }
    }
}
