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
    public class DeleteCourseWorkGradeHandlerTestFixture
    {
        private DeleteCourseWorkGradeHandlerBuilder _deleteCourseWorkGradeHandlerBuilder;
        private MockCourseWorkGradeRepositoryBuilder _courseWorkGradeRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _deleteCourseWorkGradeHandlerBuilder = new DeleteCourseWorkGradeHandlerBuilder();
            _courseWorkGradeRepositoryBuilder = new MockCourseWorkGradeRepositoryBuilder()
                .WithSuccessfulDelete();
        }

        private DeleteCourseWorkGradeHandler BuildDeleteCourseWorkGradeHandler()
        {
            return _deleteCourseWorkGradeHandlerBuilder
                .WithCourseWorkGradeRepository(_courseWorkGradeRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : DeleteCourseWorkGradeHandlerTestFixture
        {
            private DeleteCourseWorkGrade _deleteCourseWorkGrade;

            [SetUp]
            public void Setup()
            {
                _deleteCourseWorkGrade = new DeleteCourseWorkGrade(new CourseWorkGradeBuilder().Build());
            }

            [Test]
            public void It_should_call_delete_on_course_work_repository()
            {
                var deleteCourseWorkGradeHanlder = BuildDeleteCourseWorkGradeHandler();
                deleteCourseWorkGradeHanlder.Handle(_deleteCourseWorkGrade);

                _courseWorkGradeRepositoryBuilder.Mock.Verify(x => x.Delete(_deleteCourseWorkGrade.ActionAgainst));
            }
        }
    }
}
