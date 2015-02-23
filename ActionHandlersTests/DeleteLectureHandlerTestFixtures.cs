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
    public class DeleteLectureHandlerTestFixture
    {
        private DeleteLectureHandlerBuilder _deleteLectureHandlerBuilder;
        private MockLectureRepositoryBuilder _lectureRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _deleteLectureHandlerBuilder = new DeleteLectureHandlerBuilder();
            _lectureRepositoryBuilder = new MockLectureRepositoryBuilder()
                .WithSuccessfulDelete();
        }

        private DeleteLectureHandler BuildDeleteLectureHandler()
        {
            return _deleteLectureHandlerBuilder
                .WithLectureRepository(_lectureRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : DeleteLectureHandlerTestFixture
        {
            private DeleteLecture _deleteLecture;

            [SetUp]
            public void Setup()
            {
                _deleteLecture = new DeleteLecture(new LectureBuilder().Build());
                _lectureRepositoryBuilder.WithLecture(_deleteLecture.ActionAgainst);
            }

            [Test]
            public void It_should_call_delete_on_course_work_repository()
            {
                var deleteLectureHanlder = BuildDeleteLectureHandler();
                deleteLectureHanlder.Handle(_deleteLecture);

                _lectureRepositoryBuilder.Mock.Verify(x => x.Delete(_deleteLecture.ActionAgainst));
            }
        }
    }
}
