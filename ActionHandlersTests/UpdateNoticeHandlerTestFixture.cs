using ActionHandlers;
using Actions;
using Data.Repositories;
using Data.Tests.Builders.MockBuilders;
using Models.Tests.Builders;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class UpdateNoticeHandlerTestFixture
    {
        private UpdateNoticeHandlerBuilder _updateNoticeHandlerBuilder;
        private MockNoticeRepositoryBuilder _noticeRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _updateNoticeHandlerBuilder = new UpdateNoticeHandlerBuilder();
            _noticeRepositoryBuilder = new MockNoticeRepositoryBuilder()
                .WithSuccessfulUpdate();
        }

        private UpdateNoticeHandler BuildUpdateNoticeHandler()
        {
            return _updateNoticeHandlerBuilder
                .WithNoticeRepository(_noticeRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : UpdateNoticeHandlerTestFixture
        {
            private UpdateNotice _updateNotice;

            [SetUp]
            public void Setup()
            {
                _updateNotice = new UpdateNotice(new NoticeBuilder().Build());
                _noticeRepositoryBuilder.WithNotice(_updateNotice.ActionAgainst);
            }

            [Test]
            public void It_should_call_update_on_notice_repository()
            {
                var updateNoticeHanlder = BuildUpdateNoticeHandler();
                updateNoticeHanlder.Handle(_updateNotice);

                _noticeRepositoryBuilder.Mock.Verify(x => x.Update(_updateNotice.ActionAgainst));
            }
        }
    }

    public class UpdateNoticeHandlerBuilder
    {
        private INoticeRepository _noticeRepository;

        public UpdateNoticeHandler Build()
        {
            return new UpdateNoticeHandler(_noticeRepository);
        }

        public UpdateNoticeHandlerBuilder WithNoticeRepository(INoticeRepository noticeRepository)
        {
            _noticeRepository = noticeRepository;
            return this;
        }
    }
}
