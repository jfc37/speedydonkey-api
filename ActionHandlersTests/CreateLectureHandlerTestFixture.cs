using ActionHandlers;
using Actions;
using Common.Tests.Builders.MockBuilders;
using Data.Repositories;
using Data.Tests.Builders.MockBuilders;
using Models;
using Models.Tests.Builders;
using Moq;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class CreateLectureHandlerTestFixture
    {
        private CreateLectureHandlerBuilder _createLectureHandlerBuilder;
        private MockLectureRepositoryBuilder _lectureRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _createLectureHandlerBuilder = new CreateLectureHandlerBuilder();
            _lectureRepositoryBuilder = new MockLectureRepositoryBuilder()
                .WithSuccessfulCreation();
        }

        private CreateLectureHandler BuildCreateLectureHandler()
        {
            return _createLectureHandlerBuilder
                .WithLectureRepository(_lectureRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : CreateLectureHandlerTestFixture
        {
            private CreateLecture _createLecture;

            [SetUp]
            public void Setup()
            {
                _createLecture = new CreateLecture(new LectureBuilder().Build());
            }

            [Test]
            public void It_should_call_create_on_lecture_repository()
            {
                var createLectureHanlder = BuildCreateLectureHandler();
                createLectureHanlder.Handle(_createLecture);

                _lectureRepositoryBuilder.Mock.Verify(x => x.Create(_createLecture.ActionAgainst));
            }
        }
    }

    public class CreateLectureHandlerBuilder
    {
        private ILectureRepository _lectureRepository;

        public CreateLectureHandler Build()
        {
            return new CreateLectureHandler(_lectureRepository);
        }

        public CreateLectureHandlerBuilder WithLectureRepository(ILectureRepository lectureRepository)
        {
            _lectureRepository = lectureRepository;
            return this;
        }
    }
}
