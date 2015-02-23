using ActionHandlers;
using Data.Repositories;

namespace ActionHandlersTests.Builders
{
    public class DeleteNoticeHandlerBuilder
    {
        private INoticeRepository _noticeRepository;

        public DeleteNoticeHandler Build()
        {
            return new DeleteNoticeHandler(_noticeRepository);
        }

        public DeleteNoticeHandlerBuilder WithNoticeRepository(INoticeRepository noticeRepository)
        {
            _noticeRepository = noticeRepository;
            return this;
        }
    }
}