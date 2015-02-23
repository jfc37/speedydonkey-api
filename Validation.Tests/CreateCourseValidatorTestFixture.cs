using System;
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
    public class CreateCourseValidatorTestFixture
    {
        private CreateCourseValidatorBuilder _createCourseValidatorBuilder;
        private MockDbContextBuilder _contextBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _createCourseValidatorBuilder = new CreateCourseValidatorBuilder();
            _contextBuilder = new MockDbContextBuilder();
        }

        private CreateCourseValidator BuildCreateCourseValidator()
        {
            return _createCourseValidatorBuilder
                .WithContext(_contextBuilder.BuildObject())
                .Build();
        }

        public class Validate : CreateCourseValidatorTestFixture
        {
            private CourseBuilder _courseBuilder;

            [SetUp]
            public void Setup()
            {
                _contextBuilder.WithNoCourses();
                _courseBuilder = new CourseBuilder()
                    .WithValidInputs();
            }

            [Test]
            public void It_should_return_no_errors_when_course_is_valid()
            {
                _courseBuilder.WithValidInputs();

                var createCourseValidator = BuildCreateCourseValidator();
                var results = createCourseValidator.Validate(_courseBuilder.Build());

                Assert.IsTrue(results.IsValid);
            }

            [Test]
            public void It_should_require_name()
            {
                _courseBuilder.WithName(null);

                var createCourseValidator = BuildCreateCourseValidator();
                var results = createCourseValidator.Validate(_courseBuilder.Build());

                Assert.AreEqual("Name", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingName, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_name_not_be_empty()
            {
                _courseBuilder.WithName("");

                var createCourseValidator = BuildCreateCourseValidator();
                var results = createCourseValidator.Validate(_courseBuilder.Build());

                Assert.AreEqual("Name", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingName, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_name_be_unique()
            {
                const string coursename = "course";
                _contextBuilder.WithCourse(new CourseBuilder().WithName(coursename).Build());
                _courseBuilder.WithName(coursename);

                var createCourseValidator = BuildCreateCourseValidator();
                var results = createCourseValidator.Validate(_courseBuilder.Build());

                Assert.AreEqual("Name", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.DuplicateCourseName, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_start_date()
            {
                _courseBuilder.WithStartDate(DateTime.MinValue);

                var createCourseValidator = BuildCreateCourseValidator();
                var results = createCourseValidator.Validate(_courseBuilder.Build());

                Assert.AreEqual("StartDate", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingStartDate, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_have_start_date_before_end_date()
            {
                _courseBuilder
                    .WithStartDate(DateTime.Today)
                    .WithEndDate(DateTime.Today);

                var createCourseValidator = BuildCreateCourseValidator();
                var results = createCourseValidator.Validate(_courseBuilder.Build());

                Assert.AreEqual("EndDate", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.EndDateBeforeStartDate, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_grade_type()
            {
                _courseBuilder.WithGradeType(GradeType.Invalid);

                var createCourseValidator = BuildCreateCourseValidator();
                var results = createCourseValidator.Validate(_courseBuilder.Build());

                Assert.AreEqual("GradeType", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingGradeType, results.Errors.Single().ErrorMessage);
            }
        }
    }
}
