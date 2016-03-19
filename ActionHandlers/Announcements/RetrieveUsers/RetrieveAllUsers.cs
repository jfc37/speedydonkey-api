using System.Collections.Generic;
using Data.Repositories;
using Models;

namespace ActionHandlers.Announcements.RetrieveUsers
{
    public class RetrieveAllUsers : IRetrieveUsersToNotify
    {
        private readonly IRepository<User> _repository;

        public RetrieveAllUsers(IRepository<User> repository)
        {
            _repository = repository;
        }

        public IEnumerable<User> Get(Announcement announcement)
        {
            return _repository.Queryable();
        }
    }
}
