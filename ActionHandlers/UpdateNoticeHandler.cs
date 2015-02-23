using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class UpdateNoticeHandler : IActionHandler<UpdateNotice, Notice>
    {
        private readonly INoticeRepository _noticeRepository;

        public UpdateNoticeHandler(INoticeRepository noticeRepository)
        {
            _noticeRepository = noticeRepository;
        }

        public Notice Handle(UpdateNotice action)
        {
            var originalNotice = _noticeRepository.Get(action.ActionAgainst.Id);
            originalNotice.EndDate = action.ActionAgainst.EndDate;
            originalNotice.StartDate = action.ActionAgainst.StartDate;
            originalNotice.Message = action.ActionAgainst.Message;

            return _noticeRepository.Update(action.ActionAgainst);
        }
    }
}
