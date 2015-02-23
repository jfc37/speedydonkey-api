using ActionHandlers;
using Data.Repositories;

namespace ActionHandlersTests.Builders
{
    public class CreateNoticeHandlerBuilder
    {
        private INoticeRepository _noticeRepository;

        public CreateNoticeHandler Build()
        {
            return new CreateNoticeHandler(_noticeRepository);
        }

        public CreateNoticeHandlerBuilder WithNoticeRepository(INoticeRepository noticeRepository)
        {
            _noticeRepository = noticeRepository;
            return this;
        }
    }
}