using System.Linq;
using Data.Tests.Builders.MockBuilders;
using Models;
using Models.Tests.Builders;
using NUnit.Framework;
using Validation.Tests.Builders;
using Validation.Validators;

namespace Validation.Tests
{
    [TestFixture]
    public class EnrolStudentValidatorTestFixture
    {
        private EnrolStudentValidatorBuilder _enrolStudentValidatorBuilder;
        private MockPersonRepositoryBuilder<Student> _studentRepositoryBuilder;
        private MockCourseRepositoryBuilder _courseRepositoryBuilder;
            
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _enrolStudentValidatorBuilder = new EnrolStudentValidatorBuilder();
            _studentRepositoryBuilder = new MockPersonRepositoryBuilder<Student>();
            _courseRepositoryBuilder = new MockCourseRepositoryBuilder();
        }

        private EnrolStudentValidator BuildValidator()
        {
            return _enrolStudentValidatorBuilder
                .WithStudentRepository(_studentRepositoryBuilder.BuildObject())
                .WithCourseRepository(_courseRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Validate : EnrolStudentValidatorTestFixture
        {
            private PersonBuilder _personBuilder;
            private int _studentId;
            private int _courseId;
            private CourseBuilder _courseBuilder;

            [SetUp]
            public void Setup()
            {
                _studentId = 453;
                _courseId = 4776;
                _courseBuilder = new CourseBuilder().WithId(_courseId);
                _personBuilder = new PersonBuilder()
                    .WithId(_studentId)
                    .WithCourse(_courseBuilder.Build());

                _studentRepositoryBuilder = new MockPersonRepositoryBuilder<Student>()
                    .WithPerson(_personBuilder.BuildStudent());
                _courseRepositoryBuilder = new MockCourseRepositoryBuilder()
                    .WithCourse(_courseBuilder.Build());
            }

            [Test]
            public void It_should_return_no_errors_when_student_enrolment_is_valid()
            {
                _studentRepositoryBuilder.WithPerson(new PersonBuilder().WithId(_studentId).WithNoCourses().BuildStudent());

                var createCourseValidator = BuildValidator();
                var results = createCourseValidator.Validate(_personBuilder.BuildStudent());

                Assert.IsTrue(results.IsValid);
            }

            [Test]
            public void It_should_require_student_to_be_existing()
            {
                _studentRepositoryBuilder
                    .WithNoPeople();

                var enrolStudentValidator = BuildValidator();
                var results = enrolStudentValidator.Validate(_personBuilder.BuildStudent());

                Assert.AreEqual("Id", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.StudentDoesntExist, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_only_one_course_enrolled_in()
            {
                _personBuilder
                    .WithCourse(new Course())
                    .WithCourse(new Course());

                var enrolStudentValidator = BuildValidator();
                var results = enrolStudentValidator.Validate(_personBuilder.BuildStudent());

                Assert.IsNotEmpty(results.Errors.Where(x => x.PropertyName == "EnroledCourses"));
                Assert.IsNotEmpty(results.Errors.Where(x => x.ErrorMessage == ValidationMessages.EnrolingInMultipleCourses));
            }

            [Test]
            public void It_should_require_all_courses_to_be_existing()
            {
                _courseRepositoryBuilder
                    .WithNoCourses();

                var enrolStudentValidator = BuildValidator();
                var results = enrolStudentValidator.Validate(_personBuilder.BuildStudent());

                Assert.AreEqual("EnroledCourses", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.CoursesDontExist, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_all_courses_to_be_existing()
            {
                _courseRepositoryBuilder
                    .WithNoCourses();

                var enrolStudentValidator = BuildValidator();
                var results = enrolStudentValidator.Validate(_personBuilder.BuildStudent());

                Assert.AreEqual("EnroledCourses", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.CoursesDontExist, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_student_not_to_be_enrolled_in_course_already()
            {
                var student = _personBuilder
                    .WithNoCourses()
                    .WithCourse(_courseBuilder.Build())
                    .BuildStudent();
                _studentRepositoryBuilder
                    .WithPerson(student);

                var enrolStudentValidator = BuildValidator();
                var results = enrolStudentValidator.Validate(_personBuilder.BuildStudent());

                Assert.AreEqual("Id", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.StudentAlreadyEnroled, results.Errors.Single().ErrorMessage);
            }
        }
    }
}
