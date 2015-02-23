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
    public class CreateNoticeValidatorTestFixture
    {
        private CreateNoticeValidatorBuilder _createNoticeValidatorBuilder;
        private MockDbContextBuilder _contextBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _createNoticeValidatorBuilder = new CreateNoticeValidatorBuilder();
            _contextBuilder = new MockDbContextBuilder();
        }

        private CreateNoticeValidator BuildCreateNoticeValidator()
        {
            return _createNoticeValidatorBuilder
                .WithContext(_contextBuilder.BuildObject())
                .Build();
        }

        public class Validate : CreateNoticeValidatorTestFixture
        {
            private NoticeBuilder _noticeBuilder;

            [SetUp]
            public void Setup()
            {
                _contextBuilder.WithNoNotices();
                _noticeBuilder = new NoticeBuilder()
                    .WithValidInputs();
            }

            [Test]
            public void It_should_return_no_errors_when_course_is_valid()
            {
                _noticeBuilder.WithValidInputs();

                var createCourseValidator = BuildCreateNoticeValidator();
                var results = createCourseValidator.Validate(_noticeBuilder.Build());

                Assert.IsTrue(results.IsValid);
            }

            [Test]
            public void It_should_require_message()
            {
                _noticeBuilder.WithMessage(null);

                var createCourseValidator = BuildCreateNoticeValidator();
                var results = createCourseValidator.Validate(_noticeBuilder.Build());

                Assert.AreEqual("Message", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingNoticeMessage, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_have_start_date_before_end_date()
            {
                _noticeBuilder
                    .WithStartDate(DateTime.Today)
                    .WithEndDate(DateTime.Today);

                var createCourseValidator = BuildCreateNoticeValidator();
                var results = createCourseValidator.Validate(_noticeBuilder.Build());

                Assert.AreEqual("EndDate", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.EndDateBeforeStartDate, results.Errors.Single().ErrorMessage);
            }
        }
    }

    public class CreateNoticeValidatorBuilder
    {
        private ISpeedyDonkeyDbContext _context;

        public CreateNoticeValidatorBuilder WithContext(ISpeedyDonkeyDbContext context)
        {
            _context = context;
            return this;
        }

        public CreateNoticeValidator Build()
        {
            return new CreateNoticeValidator();
        }
    }
}
