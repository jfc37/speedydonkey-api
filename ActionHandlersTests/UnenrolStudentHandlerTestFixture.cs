using ActionHandlers;
using ActionHandlersTests.Builders;
using Actions;
using Data.Tests.Builders.MockBuilders;
using Models;
using Models.Tests.Builders;
using Moq;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class UnenrolStudentHandlerTestFixture
    {
        private UnenrolStudentHandlerBuilder _unenrolStudentHandlerBuilder;
        private MockPersonRepositoryBuilder<Student> _studentRepositoryBuilder;
        private MockCourseRepositoryBuilder _courseRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {

            _unenrolStudentHandlerBuilder = new UnenrolStudentHandlerBuilder();
            _studentRepositoryBuilder = new MockPersonRepositoryBuilder<Student>();
            _courseRepositoryBuilder = new MockCourseRepositoryBuilder();
        }

        private UnenrolStudentHandler BuildUnenrolStudentHandler()
        {
            return _unenrolStudentHandlerBuilder
                .WithStudentRepository(_studentRepositoryBuilder.BuildObject())
                .WithCourseRepository(_courseRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : UnenrolStudentHandlerTestFixture
        {
            private UnenrolStudent _unenrolStudent;
            private int _courseId;
            private int _studentId;

            [SetUp]
            public void Setup()
            {
                _courseId = 1;
                _studentId = 2;
                Course course = new CourseBuilder().WithId(_courseId).Build();
                _unenrolStudent = new UnenrolStudent(new PersonBuilder().WithId(_studentId).WithCourse(course).BuildStudent());

                _studentRepositoryBuilder = new MockPersonRepositoryBuilder<Student>()
                    .WithPerson(new PersonBuilder().WithId(_studentId).WithCourseGrade(new CourseGradeBuilder().WithCourse(course).Build()).BuildStudent())
                    .WithSuccessfulUpdate();
                _courseRepositoryBuilder = new MockCourseRepositoryBuilder()
                    .WithCourse(course);
            }

            [Test]
            public void It_should_call_update_on_student_repository()
            {
                var createPersonHanlder = BuildUnenrolStudentHandler();
                createPersonHanlder.Handle(_unenrolStudent);

                _studentRepositoryBuilder.Mock.Verify(x => x.Update(It.IsAny<Student>()));
            }
        }
    }
}
