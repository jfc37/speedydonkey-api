using System.Collections.Generic;
using Models;

namespace ActionHandlers.Announcements.RetrieveUsers
{
    public interface IRetrieveUsersToNotify
    {
        IEnumerable<User> Get(Announcement announcement);
    }
}