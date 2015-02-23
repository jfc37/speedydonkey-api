using System;
using System.Linq;
using Data.Repositories;
using Data.Tests.Builders.MockBuilders;
using Models.Tests.Builders;
using NUnit.Framework;
using Validation.Validators;

namespace Validation.Tests
{
    [TestFixture]
    public class UpdateExamValidatorTestFixture
    {
        private UpdateExamValidatorBuilder _updateExamValidatorBuilder;
        private MockExamRepositoryBuilder _examRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _updateExamValidatorBuilder = new UpdateExamValidatorBuilder();
            _examRepositoryBuilder = new MockExamRepositoryBuilder();
        }

        private UpdateExamValidator BuildUpdateExamValidator()
        {
            return _updateExamValidatorBuilder
                .WithExamRepository(_examRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Validate : UpdateExamValidatorTestFixture
        {
            private ExamBuilder _examBuilder;

            [SetUp]
            public void Setup()
            {
                _examBuilder = new ExamBuilder()
                    .WithValidInputs()
                    .WithId(1);
                _examRepositoryBuilder
                    .WithNoExams()
                    .WithExam(_examBuilder.Build());
            }

            [Test]
            public void It_should_return_no_errors_when_course_is_valid()
            {
                _examBuilder.WithValidInputs();

                var updateExamValidator = BuildUpdateExamValidator();
                var results = updateExamValidator.Validate(_examBuilder.Build());

                Assert.IsTrue(results.IsValid);
            }

            [Test]
            public void It_should_require_exam_be_existing()
            {
                _examBuilder.WithId(1);
                _examRepositoryBuilder = new MockExamRepositoryBuilder()
                    .WithNoExams();

                var updateExamValidator = BuildUpdateExamValidator();
                var results = updateExamValidator.Validate(_examBuilder.Build());

                Assert.AreEqual("Id", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.ExamDoesntExist, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_name()
            {
                _examBuilder.WithName(null);

                var updateExamValidator = BuildUpdateExamValidator();
                var results = updateExamValidator.Validate(_examBuilder.Build());

                Assert.AreEqual("Name", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingName, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_final_mark_percentage_be_no_more_than_100()
            {
                _examBuilder.WithFinalMarkPercentage(101);

                var updateExamValidator = BuildUpdateExamValidator();
                var results = updateExamValidator.Validate(_examBuilder.Build());

                Assert.AreEqual("FinalMarkPercentage", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.FinalMarkPercentageGreaterThan100, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_have_start_time()
            {
                _examBuilder
                    .WithStartTime(DateTime.MinValue);

                var updateCourseValidator = BuildUpdateExamValidator();
                var results = updateCourseValidator.Validate(_examBuilder.Build());

                Assert.AreEqual("StartTime", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingStartTime, results.Errors.Single().ErrorMessage);
            }
        }
    }

    public class UpdateExamValidatorBuilder
    {
        private IExamRepository _examRepository;

        public UpdateExamValidatorBuilder WithExamRepository(IExamRepository examRepository)
        {
            _examRepository = examRepository;
            return this;
        }

        public UpdateExamValidator Build()
        {
            return new UpdateExamValidator(_examRepository);
        }
    }
}
