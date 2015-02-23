using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class DeleteNoticeHandler : IActionHandler<DeleteNotice, Notice>
    {
        private readonly INoticeRepository _noticeRepository;

        public DeleteNoticeHandler(INoticeRepository noticeRepository)
        {
            _noticeRepository = noticeRepository;
        }

        public Notice Handle(DeleteNotice action)
        {
            var noticeToDelete = _noticeRepository.Get(action.ActionAgainst.Id);
            _noticeRepository.Delete(noticeToDelete);
            return noticeToDelete;
        }
    }
}
