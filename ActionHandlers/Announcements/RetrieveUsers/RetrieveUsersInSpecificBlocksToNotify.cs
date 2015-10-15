using System.Collections.Generic;
using System.Linq;
using Data.Repositories;
using Models;

namespace ActionHandlers.Announcements.RetrieveUsers
{
    public class RetrieveUsersInSpecificBlocksToNotify : IRetrieveUsersToNotify
    {
        private readonly IRepository<Block> _repository;

        public RetrieveUsersInSpecificBlocksToNotify(IRepository<Block> repository)
        {
            _repository = repository;
        }

        public IEnumerable<User> Get(Announcement announcement)
        {
            var blockIdsToNotify = announcement.Receivers.Select(x => x.Id);
            return _repository.GetAll()
                .Where(x => blockIdsToNotify.Contains(x.Id))
                .SelectMany(x => x.EnroledStudents);
        }
    }
}