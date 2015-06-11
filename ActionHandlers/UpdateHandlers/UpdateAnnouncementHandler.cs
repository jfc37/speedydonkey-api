using System.Linq;
using Action;
using Common;
using Data.Repositories;
using Models;

namespace ActionHandlers.UpdateHandlers
{
    public class UpdateAnnouncementHandler : IActionHandler<UpdateAnnouncement, Announcement>
    {
        private readonly IRepository<Announcement> _repository;
        private readonly IRepository<Block> _blockRepository;
        private readonly ICommonInterfaceCloner _cloner;

        public UpdateAnnouncementHandler(
            IRepository<Announcement> repository, IRepository<Block> blockRepository, ICommonInterfaceCloner cloner)
        {
            _repository = repository;
            _blockRepository = blockRepository;
            _cloner = cloner;
        }

        public Announcement Handle(UpdateAnnouncement action)
        {
            var originalEntity = _repository.Get(action.ActionAgainst.Id);
            _cloner.Copy(action.ActionAgainst, originalEntity);

            if (originalEntity.Receivers.DoesNotHaveSameItems(action.ActionAgainst.Receivers))
            {
                var blocks = action.ActionAgainst.Receivers.Select(b => _blockRepository.Get(b.Id)).Cast<IBlock>().ToList();
                originalEntity.Receivers = blocks;
            }

            return _repository.Update(originalEntity);

        }
    }
}
