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
    public class UpdateNoticeValidatorTestFixture
    {
        private UpdateNoticeValidatorBuilder _updateNoticeValidatorBuilder;
        private MockNoticeRepositoryBuilder _noticeRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _updateNoticeValidatorBuilder = new UpdateNoticeValidatorBuilder();
            _noticeRepositoryBuilder = new MockNoticeRepositoryBuilder();
        }

        private UpdateNoticeValidator BuildUpdateNoticeValidator()
        {
            return _updateNoticeValidatorBuilder
                .WithNoticeRepository(_noticeRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Validate : UpdateNoticeValidatorTestFixture
        {
            private NoticeBuilder _noticeBuilder;

            [SetUp]
            public void Setup()
            {
                _noticeBuilder = new NoticeBuilder()
                    .WithValidInputs()
                    .WithId(2);

                _noticeRepositoryBuilder
                    .WithNoNotices()
                    .WithNotice(_noticeBuilder.Build());
            }

            [Test]
            public void It_should_return_no_errors_when_course_is_valid()
            {
                _noticeBuilder.WithValidInputs();

                var updateCourseValidator = BuildUpdateNoticeValidator();
                var results = updateCourseValidator.Validate(_noticeBuilder.Build());

                Assert.IsTrue(results.IsValid);
            }

            [Test]
            public void It_should_require_notice_be_existing()
            {
                _noticeBuilder.WithId(1);
                _noticeRepositoryBuilder = new MockNoticeRepositoryBuilder()
                    .WithNoNotices();

                var updateNoticeValidator = BuildUpdateNoticeValidator();
                var results = updateNoticeValidator.Validate(_noticeBuilder.Build());

                Assert.AreEqual("Id", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.NoticeDoesntExist, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_message()
            {
                _noticeBuilder.WithMessage(null);

                var updateCourseValidator = BuildUpdateNoticeValidator();
                var results = updateCourseValidator.Validate(_noticeBuilder.Build());

                Assert.AreEqual("Message", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingNoticeMessage, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_have_start_date_before_end_date()
            {
                _noticeBuilder
                    .WithStartDate(DateTime.Today)
                    .WithEndDate(DateTime.Today);

                var updateCourseValidator = BuildUpdateNoticeValidator();
                var results = updateCourseValidator.Validate(_noticeBuilder.Build());

                Assert.AreEqual("EndDate", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.EndDateBeforeStartDate, results.Errors.Single().ErrorMessage);
            }
        }
    }

    public class UpdateNoticeValidatorBuilder
    {
        private INoticeRepository _noticeRepository;

        public UpdateNoticeValidatorBuilder WithNoticeRepository(INoticeRepository noticeRepository)
        {
            _noticeRepository = noticeRepository;
            return this;
        }

        public UpdateNoticeValidator Build()
        {
            return new UpdateNoticeValidator(_noticeRepository);
        }
    }
}
