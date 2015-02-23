using ActionHandlers;
using ActionHandlersTests.Builders;
using Actions;
using Data.Tests.Builders.MockBuilders;
using Models.Tests.Builders;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class DeleteCourseHandlerTestFixture
    {
        private DeleteCourseHandlerBuilder _deleteCourseHandlerBuilder;
        private MockCourseRepositoryBuilder _courseRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _deleteCourseHandlerBuilder = new DeleteCourseHandlerBuilder();
            _courseRepositoryBuilder = new MockCourseRepositoryBuilder()
                .WithSuccessfulDelete();
        }

        private DeleteCourseHandler BuildDeleteCourseHandler()
        {
            return _deleteCourseHandlerBuilder
                .WithCourseRepository(_courseRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : DeleteCourseHandlerTestFixture
        {
            private DeleteCourse _deleteCourse;

            [SetUp]
            public void Setup()
            {
                _deleteCourse = new DeleteCourse(new CourseBuilder().Build());
                _courseRepositoryBuilder.WithCourse(_deleteCourse.ActionAgainst);
            }

            [Test]
            public void It_should_call_delete_on_course_repository()
            {
                var deleteCourseHanlder = BuildDeleteCourseHandler();
                deleteCourseHanlder.Handle(_deleteCourse);

                _courseRepositoryBuilder.Mock.Verify(x => x.Delete(_deleteCourse.ActionAgainst));
            }
        }
    }
}
