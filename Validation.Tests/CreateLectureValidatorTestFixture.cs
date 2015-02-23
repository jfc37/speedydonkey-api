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
    public class CreateLectureValidatorTestFixture
    {
        private CreateLectureValidatorBuilder _createLectureValidatorBuilder;
        private MockDbContextBuilder _contextBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _createLectureValidatorBuilder = new CreateLectureValidatorBuilder();
            _contextBuilder = new MockDbContextBuilder();
        }

        private CreateLectureValidator BuildCreateLectureValidator()
        {
            return _createLectureValidatorBuilder
                .WithContext(_contextBuilder.BuildObject())
                .Build();
        }

        public class Validate : CreateLectureValidatorTestFixture
        {
            private LectureBuilder _lectureBuilder;

            [SetUp]
            public void Setup()
            {
                _contextBuilder.WithNoLectures();
                _lectureBuilder = new LectureBuilder()
                    .WithValidInputs();
            }

            [Test]
            public void It_should_return_no_errors_when_course_is_valid()
            {
                _lectureBuilder.WithValidInputs();

                var createLectureValidator = BuildCreateLectureValidator();
                var results = createLectureValidator.Validate(_lectureBuilder.Build());

                Assert.IsTrue(results.IsValid);
            }

            [Test]
            public void It_should_require_name()
            {
                _lectureBuilder.WithName(null);

                var createLectureValidator = BuildCreateLectureValidator();
                var results = createLectureValidator.Validate(_lectureBuilder.Build());

                Assert.AreEqual("Name", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingName, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_occurence()
            {
                _lectureBuilder.WithOccurence(Occurence.Invalid);

                var createLectureValidator = BuildCreateLectureValidator();
                var results = createLectureValidator.Validate(_lectureBuilder.Build());

                Assert.AreEqual("Occurence", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingOccurence, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_have_start_date_before_end_date()
            {
                _lectureBuilder
                    .WithStartDate(DateTime.Today)
                    .WithEndDate(DateTime.Today);

                var createCourseValidator = BuildCreateLectureValidator();
                var results = createCourseValidator.Validate(_lectureBuilder.Build());

                Assert.AreEqual("EndDate", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.EndDateBeforeStartDate, results.Errors.Single().ErrorMessage);
            }
        }
    }

    public class CreateLectureValidatorBuilder
    {
        private ISpeedyDonkeyDbContext _context;

        public CreateLectureValidatorBuilder WithContext(ISpeedyDonkeyDbContext context)
        {
            _context = context;
            return this;
        }

        public CreateLectureValidator Build()
        {
            return new CreateLectureValidator();
        }
    }
}
