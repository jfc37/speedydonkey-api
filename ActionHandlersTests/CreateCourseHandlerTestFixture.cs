using ActionHandlers;
using ActionHandlersTests.Builders;
using Actions;
using Data.Tests.Builders.MockBuilders;
using Models.Tests.Builders;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class CreateCourseHandlerTestFixture
    {
        private CreateCourseHandlerBuilder _createCourseHandlerBuilder;
        private MockCourseRepositoryBuilder _courseRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _createCourseHandlerBuilder = new CreateCourseHandlerBuilder();
            _courseRepositoryBuilder = new MockCourseRepositoryBuilder()
                .WithSuccessfulCreation();
        }

        private CreateCourseHandler BuildCreateCourseHandler()
        {
            return _createCourseHandlerBuilder
                .WithCourseRepository(_courseRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : CreateCourseHandlerTestFixture
        {
            private CreateCourse _createCourse;

            [SetUp]
            public void Setup()
            {
                _createCourse = new CreateCourse(new CourseBuilder().Build());
            }

            [Test]
            public void It_should_call_create_on_course_repository()
            {
                var createCourseHanlder = BuildCreateCourseHandler();
                createCourseHanlder.Handle(_createCourse);

                _courseRepositoryBuilder.Mock.Verify(x => x.Create(_createCourse.ActionAgainst));
            }
        }
    }
}
