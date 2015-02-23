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
    public class CreateExamValidatorTestFixture
    {
        private CreateExamValidatorBuilder _createExamValidatorBuilder;
        private MockDbContextBuilder _contextBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _createExamValidatorBuilder = new CreateExamValidatorBuilder();
            _contextBuilder = new MockDbContextBuilder();
        }

        private CreateExamValidator BuildCreateExamValidator()
        {
            return _createExamValidatorBuilder
                .WithContext(_contextBuilder.BuildObject())
                .Build();
        }

        public class Validate : CreateExamValidatorTestFixture
        {
            private ExamBuilder _examBuilder;

            [SetUp]
            public void Setup()
            {
                _contextBuilder.WithNoExams();
                _examBuilder = new ExamBuilder()
                    .WithValidInputs();
            }

            [Test]
            public void It_should_return_no_errors_when_course_is_valid()
            {
                _examBuilder.WithValidInputs();

                var createExamValidator = BuildCreateExamValidator();
                var results = createExamValidator.Validate(_examBuilder.Build());

                Assert.IsTrue(results.IsValid);
            }

            [Test]
            public void It_should_require_name()
            {
                _examBuilder.WithName(null);

                var createExamValidator = BuildCreateExamValidator();
                var results = createExamValidator.Validate(_examBuilder.Build());

                Assert.AreEqual("Name", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingName, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_final_mark_percentage_be_no_more_than_100()
            {
                _examBuilder.WithFinalMarkPercentage(101);

                var createExamValidator = BuildCreateExamValidator();
                var results = createExamValidator.Validate(_examBuilder.Build());

                Assert.AreEqual("FinalMarkPercentage", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.FinalMarkPercentageGreaterThan100, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_have_start_time()
            {
                _examBuilder
                    .WithStartTime(DateTime.MinValue);

                var createCourseValidator = BuildCreateExamValidator();
                var results = createCourseValidator.Validate(_examBuilder.Build());

                Assert.AreEqual("StartTime", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingStartTime, results.Errors.Single().ErrorMessage);
            }
        }
    }

    public class CreateExamValidatorBuilder
    {
        private ISpeedyDonkeyDbContext _context;

        public CreateExamValidatorBuilder WithContext(ISpeedyDonkeyDbContext context)
        {
            _context = context;
            return this;
        }

        public CreateExamValidator Build()
        {
            return new CreateExamValidator();
        }
    }
}
