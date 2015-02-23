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
    public class UpdateAssignmentValidatorTestFixture
    {
        private UpdateAssignmentValidatorBuilder _updateAssignmentValidatorBuilder;
        private MockAssignmentRepositoryBuilder _assignmentRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _updateAssignmentValidatorBuilder = new UpdateAssignmentValidatorBuilder();
            _assignmentRepositoryBuilder = new MockAssignmentRepositoryBuilder();
        }

        private UpdateAssignmentValidator BuildUpdateAssignmentValidator()
        {
            return _updateAssignmentValidatorBuilder
                .WithAssignmentRepository(_assignmentRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Validate : UpdateAssignmentValidatorTestFixture
        {
            private AssignmentBuilder _assignmentBuilder;

            [SetUp]
            public void Setup()
            {
                _assignmentRepositoryBuilder.WithNoAssignments();
                _assignmentBuilder = new AssignmentBuilder()
                    .WithValidInputs()
                    .WithId(1);

                _assignmentRepositoryBuilder.WithAssignment(_assignmentBuilder.Build());

            }

            [Test]
            public void It_should_return_no_errors_when_assignment_is_valid()
            {
                _assignmentBuilder.WithValidInputs();

                var updateAssignmentValidator = BuildUpdateAssignmentValidator();
                var results = updateAssignmentValidator.Validate(_assignmentBuilder.Build());

                Assert.IsTrue(results.IsValid);
            }

            [Test]
            public void It_should_require_assignment_be_existing()
            {
                _assignmentBuilder.WithId(1);
                _assignmentRepositoryBuilder = new MockAssignmentRepositoryBuilder()
                    .WithNoAssignments();

                var updateAssignmentValidator = BuildUpdateAssignmentValidator();
                var results = updateAssignmentValidator.Validate(_assignmentBuilder.Build());

                Assert.AreEqual("Id", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.AssignmentDoesntExist, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_name()
            {
                _assignmentBuilder.WithName(null);

                var updateAssignmentValidator = BuildUpdateAssignmentValidator();
                var results = updateAssignmentValidator.Validate(_assignmentBuilder.Build());

                Assert.AreEqual("Name", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingName, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_final_mark_percentage_be_no_more_than_100()
            {
                _assignmentBuilder.WithFinalMarkPercentage(101);

                var updateAssignmentValidator = BuildUpdateAssignmentValidator();
                var results = updateAssignmentValidator.Validate(_assignmentBuilder.Build());

                Assert.AreEqual("FinalMarkPercentage", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.FinalMarkPercentageGreaterThan100, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_have_start_date_before_end_date()
            {
                _assignmentBuilder
                    .WithStartDate(DateTime.Today)
                    .WithEndDate(DateTime.Today);

                var updateCourseValidator = BuildUpdateAssignmentValidator();
                var results = updateCourseValidator.Validate(_assignmentBuilder.Build());

                Assert.AreEqual("EndDate", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.EndDateBeforeStartDate, results.Errors.Single().ErrorMessage);
            }
        }
    }

    public class UpdateAssignmentValidatorBuilder
    {
        private IAssignmentRepository _assignmentRepository;

        public UpdateAssignmentValidator Build()
        {
            return new UpdateAssignmentValidator(_assignmentRepository);
        }

        public UpdateAssignmentValidatorBuilder WithAssignmentRepository(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
            return this;
        }
    }
}
