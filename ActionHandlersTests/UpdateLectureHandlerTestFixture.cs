using ActionHandlers;
using Actions;
using Data.Repositories;
using Data.Tests.Builders.MockBuilders;
using Models.Tests.Builders;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class UpdateLectureHandlerTestFixture
    {
        private UpdateLectureHandlerBuilder _updateLectureHandlerBuilder;
        private MockLectureRepositoryBuilder _lectureRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _updateLectureHandlerBuilder = new UpdateLectureHandlerBuilder();
            _lectureRepositoryBuilder = new MockLectureRepositoryBuilder()
                .WithSuccessfulUpdate();
        }

        private UpdateLectureHandler BuildUpdateLectureHandler()
        {
            return _updateLectureHandlerBuilder
                .WithLectureRepository(_lectureRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : UpdateLectureHandlerTestFixture
        {
            private UpdateLecture _updateLecture;

            [SetUp]
            public void Setup()
            {
                _updateLecture = new UpdateLecture(new LectureBuilder().Build());
                _lectureRepositoryBuilder.WithLecture(_updateLecture.ActionAgainst);
            }

            [Test]
            public void It_should_call_update_on_lecture_repository()
            {
                var updateLectureHanlder = BuildUpdateLectureHandler();
                updateLectureHanlder.Handle(_updateLecture);

                _lectureRepositoryBuilder.Mock.Verify(x => x.Update(_updateLecture.ActionAgainst));
            }
        }
    }

    public class UpdateLectureHandlerBuilder
    {
        private ILectureRepository _lectureRepository;

        public UpdateLectureHandler Build()
        {
            return new UpdateLectureHandler(_lectureRepository);
        }

        public UpdateLectureHandlerBuilder WithLectureRepository(ILectureRepository lectureRepository)
        {
            _lectureRepository = lectureRepository;
            return this;
        }
    }
}
