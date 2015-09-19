using System.Linq;
using Action;
using Common;
using Common.Extensions;
using Data.Repositories;
using Models;

namespace ActionHandlers.UpdateHandlers
{
    public class UpdateAnnouncementHandler : IActionHandler<UpdateAnnouncement, Announcement>
    {
        private readonly IRepository<Announcement> _repository;
        private readonly IRepository<Block> _blockRepository;

        public UpdateAnnouncementHandler(
            IRepository<Announcement> repository, IRepository<Block> blockRepository)
        {
            _repository = repository;
            _blockRepository = blockRepository;
        }

        public Announcement Handle(UpdateAnnouncement action)
        {
            var originalEntity = _repository.Get(action.ActionAgainst.Id);
            new CommonInterfaceCloner().Copy(action.ActionAgainst, originalEntity);

            if (originalEntity.Receivers.DoesNotHaveSameItemIds(action.ActionAgainst.Receivers))
            {
                var blocks = action.ActionAgainst.Receivers.Select(b => _blockRepository.Get(b.Id)).ToList();
                originalEntity.Receivers = blocks;
            }

            return _repository.Update(originalEntity);

        }
    }
}
