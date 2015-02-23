using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class CreateNoticeHandler : IActionHandler<CreateNotice, Notice>
    {
        private readonly INoticeRepository _noticeRepository;

        public CreateNoticeHandler(INoticeRepository noticeRepository)
        {
            _noticeRepository = noticeRepository;
        }

        public Notice Handle(CreateNotice action)
        {
            return _noticeRepository.Create(action.ActionAgainst);
        }
    }
}
