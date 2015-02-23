using ActionHandlers;
using Actions;
using Data.Repositories;
using Data.Tests.Builders.MockBuilders;
using Models.Tests.Builders;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class UpdateCourseHandlerTestFixture
    {
        private UpdateCourseHandlerBuilder _createCourseHandlerBuilder;
        private MockCourseRepositoryBuilder _courseRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _createCourseHandlerBuilder = new UpdateCourseHandlerBuilder();
            _courseRepositoryBuilder = new MockCourseRepositoryBuilder()
                .WithSuccessfulUpdate();
        }

        private UpdateCourseHandler BuildUpdateCourseHandler()
        {
            return _createCourseHandlerBuilder
                .WithCourseRepository(_courseRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : UpdateCourseHandlerTestFixture
        {
            private UpdateCourse _createCourse;

            [SetUp]
            public void Setup()
            {
                _createCourse = new UpdateCourse(new CourseBuilder().Build());
                _courseRepositoryBuilder.WithCourse(_createCourse.ActionAgainst);
            }

            [Test]
            public void It_should_call_update_on_course_repository()
            {
                var createCourseHanlder = BuildUpdateCourseHandler();
                createCourseHanlder.Handle(_createCourse);

                _courseRepositoryBuilder.Mock.Verify(x => x.Update(_createCourse.ActionAgainst));
            }
        }
    }

    public class UpdateCourseHandlerBuilder
    {
        private ICourseRepository _courseRepository;

        public UpdateCourseHandler Build()
        {
            return new UpdateCourseHandler(_courseRepository);
        }

        public UpdateCourseHandlerBuilder WithCourseRepository(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            return this;
        }
    }
}
