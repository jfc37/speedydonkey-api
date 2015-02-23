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
    public class UpdateCourseWorkGradeValidatorTestFixture
    {
        private UpdateCourseWorkGradeValidatorBuilder _updateCourseWorkGradeValidatorBuilder;
        private MockPersonRepositoryBuilder<Student> _studentRepositoryBuilder;
        private MockCourseWorkGradeRepositoryBuilder _courseWorkGradeRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _updateCourseWorkGradeValidatorBuilder = new UpdateCourseWorkGradeValidatorBuilder();
            _studentRepositoryBuilder = new MockPersonRepositoryBuilder<Student>();
            _courseWorkGradeRepositoryBuilder = new MockCourseWorkGradeRepositoryBuilder();
        }

        private UpdateCourseWorkGradeValidator BuildValidator()
        {
            return _updateCourseWorkGradeValidatorBuilder
                .WithCourseWorkGradeRepository(_courseWorkGradeRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Validate : UpdateCourseWorkGradeValidatorTestFixture
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

                _courseWorkGradeBuilder = new CourseWorkGradeBuilder()
                    .WithStudentId(_studentId)
                    .WithCourseWorkId(_courseWorkId)
                    .WithCourseId(_courseId);
            }

            [Test]
            public void It_should_return_no_errors_when_updating_course_work_grade_is_valid()
            {
                _courseWorkGradeRepositoryBuilder = new MockCourseWorkGradeRepositoryBuilder()
                    .WithCourseWorkGrade(_courseWorkGradeBuilder.Build());

                var createCourseValidator = BuildValidator();
                var results = createCourseValidator.Validate(_courseWorkGradeBuilder.Build());

                Assert.IsTrue(results.IsValid);
            }

            [Test]
            public void It_should_require_course_work_grade_be_existing()
            {
                _courseWorkGradeRepositoryBuilder
                    .WithNoCourseWorkGrades();

                var updateCourseWorkGradeValidator = BuildValidator();
                var results = updateCourseWorkGradeValidator.Validate(_courseWorkGradeBuilder.Build());

                Assert.AreEqual("Id", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.CourseWorkGradeDoesntExist, results.Errors.Single().ErrorMessage);
            }
        }
    }
}
