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
    public class AddCourseWorkGradeValidatorTestFixture
    {
        private AddCourseWorkGradeValidatorBuilder _addCourseWorkGradeValidatorBuilder;
        private MockPersonRepositoryBuilder<Student> _studentRepositoryBuilder;
        private MockCourseRepositoryBuilder _courseRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _addCourseWorkGradeValidatorBuilder = new AddCourseWorkGradeValidatorBuilder();
            _studentRepositoryBuilder = new MockPersonRepositoryBuilder<Student>();
            _courseRepositoryBuilder = new MockCourseRepositoryBuilder();
        }

        private AddCourseWorkGradeValidator BuildValidator()
        {
            return _addCourseWorkGradeValidatorBuilder
                .WithStudentRepository(_studentRepositoryBuilder.BuildObject())
                .WithCourseRepository(_courseRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Validate : AddCourseWorkGradeValidatorTestFixture
        {
            private PersonBuilder _personBuilder;
            private int _studentId;
            private int _courseId;
            private int _courseWorkId;
            private CourseBuilder _courseBuilder;

            private CourseWorkGradeBuilder _courseWorkGradeBuilder;

            [SetUp]
            public void Setup()
            {
                _studentId = 453;
                _courseId = 4776;
                _courseWorkId = 656;
                _courseBuilder = new CourseBuilder()
                    .WithId(_courseId)
                    .WithAssignment(new Assignment { Id = _courseWorkId });
                _personBuilder = new PersonBuilder()
                    .WithId(_studentId)
                    .WithCourse(_courseBuilder.Build())
                    .WithCourseGrade(new CourseGrade { Course = new Course { Id = _courseId } });

                _studentRepositoryBuilder = new MockPersonRepositoryBuilder<Student>()
                    .WithPerson(_personBuilder.BuildStudent());
                _courseRepositoryBuilder = new MockCourseRepositoryBuilder()
                    .WithCourse(_courseBuilder.Build());

                _courseWorkGradeBuilder = new CourseWorkGradeBuilder()
                    .WithStudentId(_studentId)
                    .WithCourseWorkId(_courseWorkId)
                    .WithCourseId(_courseId);
            }

            [Test]
            public void It_should_return_no_errors_when_adding_course_work_grade_is_valid()
            {
                _studentRepositoryBuilder
                    .WithPerson(new PersonBuilder().WithId(_studentId).WithNoCourses().BuildStudent());

                _courseRepositoryBuilder = new MockCourseRepositoryBuilder()
                    .WithCourse(_courseBuilder.Build());

                var createCourseValidator = BuildValidator();
                var results = createCourseValidator.Validate(_courseWorkGradeBuilder.Build());

                Assert.IsTrue(results.IsValid);
            }

            [Test]
            public void It_should_require_student_to_be_existing()
            {
                _studentRepositoryBuilder
                    .WithNoPeople();

                var addCourseWorkGradeValidator = BuildValidator();
                var results = addCourseWorkGradeValidator.Validate(_courseWorkGradeBuilder.Build());

                Assert.AreEqual("CourseGrade.Student.Id", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.StudentDoesntExist, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_course_to_be_existing()
            {
                _courseRepositoryBuilder
                    .WithNoCourses();

                var addCourseWorkGradeValidator = BuildValidator();
                var results = addCourseWorkGradeValidator.Validate(_courseWorkGradeBuilder.Build());

                Assert.AreEqual("CourseGrade.Course", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.CoursesDontExist, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_course_work_to_be_existing()
            {
                _studentRepositoryBuilder
                    .WithPerson(new PersonBuilder().WithId(_studentId).WithNoCourses().BuildStudent());
                _courseRepositoryBuilder.WithCourse(new CourseBuilder().WithId(_courseId).WithNoAssignments().WithNoExams().Build());

                var addCourseWorkGradeValidator = BuildValidator();
                var results = addCourseWorkGradeValidator.Validate(_courseWorkGradeBuilder.Build());

                Assert.AreEqual("CourseWork", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.CourseWorkDoesntExist, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_student_not_already_have_grade_for_course_work()
            {
                _personBuilder
                    .WithNoCourseGrades()
                    .WithCourseGrade(new CourseGrade { Course = new Course { Id = _courseId }, CourseWorkGrades = new []{new CourseWorkGrade{CourseWork = new Assignment{Id = _courseWorkId}}}});
                _studentRepositoryBuilder
                    .WithNoPeople()
                    .WithPerson(_personBuilder.BuildStudent());

                var addCourseWorkGradeValidator = BuildValidator();
                var results = addCourseWorkGradeValidator.Validate(_courseWorkGradeBuilder.Build());

                Assert.AreEqual("CourseWork", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.StudentAlreadyHasGradeForCourseWork, results.Errors.Single().ErrorMessage);
            }
        }
    }
}
