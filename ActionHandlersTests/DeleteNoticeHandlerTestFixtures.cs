using ActionHandlers;
using ActionHandlersTests.Builders;
using Actions;
using Data.Tests.Builders.MockBuilders;
using Models;
using Models.Tests.Builders;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class DeleteNoticeHandlerTestFixture
    {
        private DeleteNoticeHandlerBuilder _deleteNoticeHandlerBuilder;
        private MockNoticeRepositoryBuilder _noticeRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _deleteNoticeHandlerBuilder = new DeleteNoticeHandlerBuilder();
            _noticeRepositoryBuilder = new MockNoticeRepositoryBuilder()
                .WithSuccessfulDelete();
        }

        private DeleteNoticeHandler BuildDeleteNoticeHandler()
        {
            return _deleteNoticeHandlerBuilder
                .WithNoticeRepository(_noticeRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : DeleteNoticeHandlerTestFixture
        {
            private DeleteNotice _deleteNotice;

            [SetUp]
            public void Setup()
            {
                _deleteNotice = new DeleteNotice(new NoticeBuilder().Build());
                _noticeRepositoryBuilder.WithNotice(_deleteNotice.ActionAgainst);
            }

            [Test]
            public void It_should_call_delete_on_course_work_repository()
            {
                var deleteNoticeHanlder = BuildDeleteNoticeHandler();
                deleteNoticeHanlder.Handle(_deleteNotice);

                _noticeRepositoryBuilder.Mock.Verify(x => x.Delete(_deleteNotice.ActionAgainst));
            }
        }
    }
}
