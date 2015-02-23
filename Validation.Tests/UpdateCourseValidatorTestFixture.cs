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
    public class UpdateCourseValidatorTestFixture
    {
        private UpdateCourseValidatorBuilder _updateCourseValidatorBuilder;
        private MockCourseRepositoryBuilder _courseRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _updateCourseValidatorBuilder = new UpdateCourseValidatorBuilder();
            _courseRepositoryBuilder = new MockCourseRepositoryBuilder();
        }

        private UpdateCourseValidator BuildUpdateCourseValidator()
        {
            return _updateCourseValidatorBuilder
                .WithCourseRepository(_courseRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Validate : UpdateCourseValidatorTestFixture
        {
            private CourseBuilder _courseBuilder;
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _courseId = 1;
                _courseBuilder = new CourseBuilder()
                    .WithValidInputs()
                    .WithId(_courseId);
                _courseRepositoryBuilder
                    .WithNoCourses()
                    .WithCourse(_courseBuilder.Build());
            }

            [Test]
            public void It_should_return_no_errors_when_course_is_valid()
            {
                _courseBuilder
                    .WithValidInputs()
                    .WithId(_courseId);

                var updateCourseValidator = BuildUpdateCourseValidator();
                var results = updateCourseValidator.Validate(_courseBuilder.Build());

                Assert.IsTrue(results.IsValid);
            }

            [Test]
            public void It_should_require_course_be_existing()
            {
                _courseBuilder.WithId(1);
                _courseRepositoryBuilder
                    .WithNoCourses();

                var updateCourseValidator = BuildUpdateCourseValidator();
                var results = updateCourseValidator.Validate(_courseBuilder.Build());

                Assert.AreEqual("Id", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.CourseDoesntExist, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_name()
            {
                _courseBuilder.WithName(null);

                var updateCourseValidator = BuildUpdateCourseValidator();
                var results = updateCourseValidator.Validate(_courseBuilder.Build());

                Assert.AreEqual("Name", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingName, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_name_not_be_empty()
            {
                _courseBuilder.WithName("");

                var updateCourseValidator = BuildUpdateCourseValidator();
                var results = updateCourseValidator.Validate(_courseBuilder.Build());

                Assert.AreEqual("Name", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingName, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_name_be_unique()
            {
                const string coursename = "course";
                _courseRepositoryBuilder.WithCourse(new CourseBuilder().WithName(coursename).Build());
                _courseBuilder.WithName(coursename);

                var updateCourseValidator = BuildUpdateCourseValidator();
                var results = updateCourseValidator.Validate(_courseBuilder.Build());

                Assert.AreEqual("Name", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.DuplicateCourseName, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_start_date()
            {
                _courseBuilder.WithStartDate(DateTime.MinValue);

                var updateCourseValidator = BuildUpdateCourseValidator();
                var results = updateCourseValidator.Validate(_courseBuilder.Build());

                Assert.AreEqual("StartDate", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingStartDate, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_have_start_date_before_end_date()
            {
                _courseBuilder
                    .WithStartDate(DateTime.Today)
                    .WithEndDate(DateTime.Today);

                var updateCourseValidator = BuildUpdateCourseValidator();
                var results = updateCourseValidator.Validate(_courseBuilder.Build());

                Assert.AreEqual("EndDate", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.EndDateBeforeStartDate, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_grade_type()
            {
                _courseBuilder.WithGradeType(GradeType.Invalid);

                var updateCourseValidator = BuildUpdateCourseValidator();
                var results = updateCourseValidator.Validate(_courseBuilder.Build());

                Assert.AreEqual("GradeType", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingGradeType, results.Errors.Single().ErrorMessage);
            }
        }
    }
}
