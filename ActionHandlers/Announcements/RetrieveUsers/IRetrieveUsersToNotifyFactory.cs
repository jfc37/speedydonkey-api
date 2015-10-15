using Models;

namespace ActionHandlers.Announcements.RetrieveUsers
{
    public interface IRetrieveUsersToNotifyFactory
    {
        IRetrieveUsersToNotify Create(Announcement announcement);
    }
}