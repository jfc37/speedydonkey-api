using ActionHandlers;
using Data.Repositories;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Tests.Builders
{
    public class NoticeApiControllerBuilder
    {
        private INoticeRepository _noticeRepository;
        private IActionHandlerOverlord _actionHandlerOverlord;
        private IModelFactory _modelFactory;

        public NoticeApiControllerBuilder WithNoticeRepository(INoticeRepository noticeRepository)
        {
            _noticeRepository = noticeRepository;
            return this;
        }

        public NoticeApiControllerBuilder WithActionHandlerOverlord(IActionHandlerOverlord actionHandlerOverlord)
        {
            _actionHandlerOverlord = actionHandlerOverlord;
            return this;
        }

        public NoticeApiControllerBuilder WithModelFactory(IModelFactory modelFactory)
        {
            _modelFactory = modelFactory;
            return this;
        }

        public NoticeApiController Build()
        {
            return new NoticeApiController(_noticeRepository, _actionHandlerOverlord, _modelFactory);
        }
    }
}