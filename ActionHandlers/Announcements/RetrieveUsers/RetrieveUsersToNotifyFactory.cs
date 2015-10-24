using Data.Repositories;
using Models;

namespace ActionHandlers.Announcements.RetrieveUsers
{
    public class RetrieveUsersToNotifyFactory : IRetrieveUsersToNotifyFactory
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Block> _blockRepository;

        public RetrieveUsersToNotifyFactory(IRepository<User> userRepository, IRepository<Block> blockRepository)
        {
            _userRepository = userRepository;
            _blockRepository = blockRepository;
        }

        public IRetrieveUsersToNotify Create(Announcement announcement)
        {
            if (announcement.NotifyAll)
                return new RetrieveAllUsers(_userRepository);

            return new RetrieveUsersInSpecificBlocksToNotify(_blockRepository);
        }
    }
}