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
    public class EnrolStudentHandlerTestFixture
    {
        private EnrolStudentHandlerBuilder _enrolStudentHandlerBuilder;
        private MockPersonRepositoryBuilder<Student> _studentRepositoryBuilder;
        private MockCourseRepositoryBuilder _courseRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {

            _enrolStudentHandlerBuilder = new EnrolStudentHandlerBuilder();
            _studentRepositoryBuilder = new MockPersonRepositoryBuilder<Student>();
            _courseRepositoryBuilder = new MockCourseRepositoryBuilder();
        }

        private EnrolStudentHandler BuildEnrolStudentHandler()
        {
            return _enrolStudentHandlerBuilder
                .WithStudentRepository(_studentRepositoryBuilder.BuildObject())
                .WithCourseRepository(_courseRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : EnrolStudentHandlerTestFixture
        {
            private EnrolStudent _enrolStudent;
            private int _courseId;
            private int _studentId;

            [SetUp]
            public void Setup()
            {
                _courseId = 234;
                _studentId = 4454;
                _enrolStudent = new EnrolStudent(new PersonBuilder().WithId(_studentId).WithCourse(new CourseBuilder().WithId(_courseId).Build()).BuildStudent());

                _studentRepositoryBuilder = new MockPersonRepositoryBuilder<Student>()
                    .WithPerson(new PersonBuilder().WithId(_studentId).BuildStudent())
                    .WithSuccessfulUpdate();
                _courseRepositoryBuilder = new MockCourseRepositoryBuilder()
                    .WithCourse(new CourseBuilder().WithId(_courseId).Build());
            }

            [Test]
            public void It_should_call_update_on_student_repository()
            {
                var createPersonHanlder = BuildEnrolStudentHandler();
                createPersonHanlder.Handle(_enrolStudent);

                _studentRepositoryBuilder.Mock.Verify(x => x.Update(It.IsAny<Student>()));
            }
        }
    }
}
