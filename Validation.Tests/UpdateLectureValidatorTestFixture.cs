using System;
using System.Linq;
using Data.Repositories;
using Data.Tests.Builders.MockBuilders;
using Models;
using Models.Tests.Builders;
using NUnit.Framework;
using Validation.Validators;

namespace Validation.Tests
{
    [TestFixture]
    public class UpdateLectureValidatorTestFixture
    {
        private UpdateLectureValidatorBuilder _updateLectureValidatorBuilder;
        private MockLectureRepositoryBuilder _lectureRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _updateLectureValidatorBuilder = new UpdateLectureValidatorBuilder();
            _lectureRepositoryBuilder = new MockLectureRepositoryBuilder();
        }

        private UpdateLectureValidator BuildUpdateLectureValidator()
        {
            return _updateLectureValidatorBuilder
                .WithLectureRepository(_lectureRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Validate : UpdateLectureValidatorTestFixture
        {
            private LectureBuilder _lectureBuilder;

            [SetUp]
            public void Setup()
            {
                _lectureBuilder = new LectureBuilder()
                    .WithValidInputs();
                _lectureRepositoryBuilder
                    .WithNoLectures()
                    .WithLecture(_lectureBuilder.Build());
            }

            [Test]
            public void It_should_return_no_errors_when_course_is_valid()
            {
                _lectureBuilder.WithValidInputs();

                var updateLectureValidator = BuildUpdateLectureValidator();
                var results = updateLectureValidator.Validate(_lectureBuilder.Build());

                Assert.IsTrue(results.IsValid);
            }

            [Test]
            public void It_should_require_lecture_be_existing()
            {
                _lectureBuilder.WithId(1);
                _lectureRepositoryBuilder = new MockLectureRepositoryBuilder()
                    .WithNoLectures();

                var updateLectureValidator = BuildUpdateLectureValidator();
                var results = updateLectureValidator.Validate(_lectureBuilder.Build());

                Assert.AreEqual("Id", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.LectureDoesntExist, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_name()
            {
                _lectureBuilder.WithName(null);

                var updateLectureValidator = BuildUpdateLectureValidator();
                var results = updateLectureValidator.Validate(_lectureBuilder.Build());

                Assert.AreEqual("Name", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingName, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_occurence()
            {
                _lectureBuilder.WithOccurence(Occurence.Invalid);

                var updateLectureValidator = BuildUpdateLectureValidator();
                var results = updateLectureValidator.Validate(_lectureBuilder.Build());

                Assert.AreEqual("Occurence", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingOccurence, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_have_start_date_before_end_date()
            {
                _lectureBuilder
                    .WithStartDate(DateTime.Today)
                    .WithEndDate(DateTime.Today);

                var updateCourseValidator = BuildUpdateLectureValidator();
                var results = updateCourseValidator.Validate(_lectureBuilder.Build());

                Assert.AreEqual("EndDate", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.EndDateBeforeStartDate, results.Errors.Single().ErrorMessage);
            }
        }
    }

    public class UpdateLectureValidatorBuilder
    {
        private ILectureRepository _lectureRepository;

        public UpdateLectureValidatorBuilder WithLectureRepository(ILectureRepository lectureRepository)
        {
            _lectureRepository = lectureRepository;
            return this;
        }

        public UpdateLectureValidator Build()
        {
            return new UpdateLectureValidator(_lectureRepository);
        }
    }
}
