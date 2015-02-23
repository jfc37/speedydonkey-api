using System;
using System.Linq;
using Data;
using Data.Tests.Builders.MockBuilders;
using Models;
using Models.Tests.Builders;
using NUnit.Framework;
using Validation.Validators;

namespace Validation.Tests
{
    [TestFixture]
    public class CreateAssignmentValidatorTestFixture
    {
        private CreateAssignmentValidatorBuilder _createAssignmentValidatorBuilder;
        private MockDbContextBuilder _contextBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _createAssignmentValidatorBuilder = new CreateAssignmentValidatorBuilder();
            _contextBuilder = new MockDbContextBuilder();
        }

        private CreateAssignmentValidator BuildCreateAssignmentValidator()
        {
            return _createAssignmentValidatorBuilder
                .WithContext(_contextBuilder.BuildObject())
                .Build();
        }

        public class Validate : CreateAssignmentValidatorTestFixture
        {
            private AssignmentBuilder _assignmentBuilder;

            [SetUp]
            public void Setup()
            {
                _contextBuilder.WithNoAssignments();
                _assignmentBuilder = new AssignmentBuilder()
                    .WithValidInputs();
            }

            [Test]
            public void It_should_return_no_errors_when_course_is_valid()
            {
                _assignmentBuilder.WithValidInputs();

                var createAssignmentValidator = BuildCreateAssignmentValidator();
                var results = createAssignmentValidator.Validate(_assignmentBuilder.Build());

                Assert.IsTrue(results.IsValid);
            }

            [Test]
            public void It_should_require_name()
            {
                _assignmentBuilder.WithName(null);

                var createAssignmentValidator = BuildCreateAssignmentValidator();
                var results = createAssignmentValidator.Validate(_assignmentBuilder.Build());

                Assert.AreEqual("Name", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingName, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_final_mark_percentage_be_no_more_than_100()
            {
                _assignmentBuilder.WithFinalMarkPercentage(101);

                var createAssignmentValidator = BuildCreateAssignmentValidator();
                var results = createAssignmentValidator.Validate(_assignmentBuilder.Build());

                Assert.AreEqual("FinalMarkPercentage", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.FinalMarkPercentageGreaterThan100, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_have_start_date_before_end_date()
            {
                _assignmentBuilder
                    .WithStartDate(DateTime.Today)
                    .WithEndDate(DateTime.Today);

                var createCourseValidator = BuildCreateAssignmentValidator();
                var results = createCourseValidator.Validate(_assignmentBuilder.Build());

                Assert.AreEqual("EndDate", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.EndDateBeforeStartDate, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_start_date()
            {
                _assignmentBuilder.WithStartDate(DateTime.MinValue);

                var createCourseValidator = BuildCreateAssignmentValidator();
                var results = createCourseValidator.Validate(_assignmentBuilder.Build());

                Assert.AreEqual("StartDate", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingStartDate, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_have_valid_grade_type()
            {
                _assignmentBuilder
                    .WithGradeType(GradeType.Invalid);

                var createCourseValidator = BuildCreateAssignmentValidator();
                var results = createCourseValidator.Validate(_assignmentBuilder.Build());

                Assert.AreEqual("GradeType", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingGradeType, results.Errors.Single().ErrorMessage);
            }
        }
    }

    public class CreateAssignmentValidatorBuilder
    {
        private ISpeedyDonkeyDbContext _context;

        public CreateAssignmentValidatorBuilder WithContext(ISpeedyDonkeyDbContext context)
        {
            _context = context;
            return this;
        }

        public CreateAssignmentValidator Build()
        {
            return new CreateAssignmentValidator();
        }
    }
}
