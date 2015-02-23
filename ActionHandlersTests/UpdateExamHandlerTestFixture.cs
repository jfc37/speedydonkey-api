using ActionHandlers;
using Actions;
using Data.Repositories;
using Data.Tests.Builders.MockBuilders;
using Models.Tests.Builders;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class UpdateExamHandlerTestFixture
    {
        private UpdateExamHandlerBuilder _createExamHandlerBuilder;
        private MockExamRepositoryBuilder _examRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _createExamHandlerBuilder = new UpdateExamHandlerBuilder();
            _examRepositoryBuilder = new MockExamRepositoryBuilder()
                .WithSuccessfulUpdate();
        }

        private UpdateExamHandler BuildUpdateExamHandler()
        {
            return _createExamHandlerBuilder
                .WithExamRepository(_examRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : UpdateExamHandlerTestFixture
        {
            private UpdateExam _createExam;

            [SetUp]
            public void Setup()
            {
                _createExam = new UpdateExam(new ExamBuilder().Build());
                _examRepositoryBuilder.WithExam(_createExam.ActionAgainst);
            }

            [Test]
            public void It_should_call_update_on_exam_repository()
            {
                var createExamHanlder = BuildUpdateExamHandler();
                createExamHanlder.Handle(_createExam);

                _examRepositoryBuilder.Mock.Verify(x => x.Update(_createExam.ActionAgainst));
            }
        }
    }

    public class UpdateExamHandlerBuilder
    {
        private IExamRepository _examRepository;

        public UpdateExamHandler Build()
        {
            return new UpdateExamHandler(_examRepository);
        }

        public UpdateExamHandlerBuilder WithExamRepository(IExamRepository examRepository)
        {
            _examRepository = examRepository;
            return this;
        }
    }
}
