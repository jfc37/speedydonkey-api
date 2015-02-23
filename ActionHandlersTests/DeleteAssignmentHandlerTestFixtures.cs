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
    public class DeleteCourseWorkHandlerTestFixture
    {
        private DeleteCourseWorkHandlerBuilder _deleteCourseWorkHandlerBuilder;
        private MockCourseWorkRepositoryBuilder<CourseWork> _courseWorkRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _deleteCourseWorkHandlerBuilder = new DeleteCourseWorkHandlerBuilder();
            _courseWorkRepositoryBuilder = new MockCourseWorkRepositoryBuilder<CourseWork>()
                .WithSuccessfulDelete();
        }

        private DeleteCourseWorkHandler BuildDeleteCourseWorkHandler()
        {
            return _deleteCourseWorkHandlerBuilder
                .WithCourseWorkRepository(_courseWorkRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : DeleteCourseWorkHandlerTestFixture
        {
            private DeleteCourseWork _deleteCourseWork;

            [SetUp]
            public void Setup()
            {
                _deleteCourseWork = new DeleteCourseWork(new AssignmentBuilder().Build());
                _courseWorkRepositoryBuilder.WithCourseWork(_deleteCourseWork.ActionAgainst);
            }

            [Test]
            public void It_should_call_delete_on_course_work_repository()
            {
                var deleteAssignmentHanlder = BuildDeleteCourseWorkHandler();
                deleteAssignmentHanlder.Handle(_deleteCourseWork);

                _courseWorkRepositoryBuilder.Mock.Verify(x => x.Delete(_deleteCourseWork.ActionAgainst));
            }
        }
    }
}
