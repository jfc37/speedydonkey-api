using ActionHandlers;
using ActionHandlersTests.Builders;
using Actions;
using Data.Tests.Builders.MockBuilders;
using Models;
using Moq;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class AddCourseWorkGradeHandlerTestFixture
    {
        private AddCourseWorkGradeHandlerBuilder _addCourseWorkGradeHandlerBuilder;
        private MockCourseWorkGradeRepositoryBuilder _courseWorkGradeRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {

            _addCourseWorkGradeHandlerBuilder = new AddCourseWorkGradeHandlerBuilder();
            _courseWorkGradeRepositoryBuilder = new MockCourseWorkGradeRepositoryBuilder();
        }

        private AddCourseWorkGradeHandler BuildAddCourseWorkGradeHandler()
        {
            return _addCourseWorkGradeHandlerBuilder
                .WithCourseWorkGradeRepository(_courseWorkGradeRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : AddCourseWorkGradeHandlerTestFixture
        {
            private AddCourseWorkGrade _addCourseWorkGrade;

            [SetUp]
            public void Setup()
            {
                _addCourseWorkGrade = new AddCourseWorkGrade(new CourseWorkGrade());

                _courseWorkGradeRepositoryBuilder = new MockCourseWorkGradeRepositoryBuilder()
                    .WithSuccessfulCreation();
            }

            [Test]
            public void It_should_call_update_on_student_repository()
            {
                var createPersonHanlder = BuildAddCourseWorkGradeHandler();
                createPersonHanlder.Handle(_addCourseWorkGrade);

                _courseWorkGradeRepositoryBuilder.Mock.Verify(x => x.Create(It.IsAny<CourseWorkGrade>()));
            }
        }
    }
}
