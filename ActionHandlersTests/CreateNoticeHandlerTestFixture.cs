using ActionHandlers;
using ActionHandlersTests.Builders;
using Actions;
using Data.Tests.Builders.MockBuilders;
using Models.Tests.Builders;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class CreateNoticeHandlerTestFixture
    {
        private CreateNoticeHandlerBuilder _createNoticeHandlerBuilder;
        private MockNoticeRepositoryBuilder _noticeRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _createNoticeHandlerBuilder = new CreateNoticeHandlerBuilder();
            _noticeRepositoryBuilder = new MockNoticeRepositoryBuilder()
                .WithSuccessfulCreation();
        }

        private CreateNoticeHandler BuildCreateNoticeHandler()
        {
            return _createNoticeHandlerBuilder
                .WithNoticeRepository(_noticeRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : CreateNoticeHandlerTestFixture
        {
            private CreateNotice _createNotice;

            [SetUp]
            public void Setup()
            {
                _createNotice = new CreateNotice(new NoticeBuilder().Build());
            }

            [Test]
            public void It_should_call_create_on_notice_repository()
            {
                var createNoticeHanlder = BuildCreateNoticeHandler();
                createNoticeHanlder.Handle(_createNotice);

                _noticeRepositoryBuilder.Mock.Verify(x => x.Create(_createNotice.ActionAgainst));
            }
        }
    }
}
