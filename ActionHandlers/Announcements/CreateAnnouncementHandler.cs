using System.Linq;
using Action;
using ActionHandlers.Announcements.RetrieveUsers;
using Models;
using Notification.NotificationHandlers;
using Notification.Notifications;

namespace ActionHandlers.Announcements
{
    public class CreateAnnouncementHandler : IActionHandler<CreateAnnouncement, Announcement>
    {
        private readonly IRetrieveUsersToNotifyFactory _retrieveUsersToNotifyFactory;
        private readonly INotificationHandler<EmailAnnouncement> _notificationHandler;

        public CreateAnnouncementHandler(IRetrieveUsersToNotifyFactory retrieveUsersToNotifyFactory,
            INotificationHandler<EmailAnnouncement> notificationHandler)
        {
            _retrieveUsersToNotifyFactory = retrieveUsersToNotifyFactory;
            _notificationHandler = notificationHandler;
        }

        public Announcement Handle(CreateAnnouncement action)
        {
            var usersToNotify = _retrieveUsersToNotifyFactory.Create(action.ActionAgainst)
                .Get(action.ActionAgainst)
                .ToList();

            usersToNotify.ForEach(x =>
                _notificationHandler.Handle(
                    new EmailAnnouncement(x, action.ActionAgainst.Message, action.ActionAgainst.Subject)));

            action.NumberOfUsersEmailed = usersToNotify.Count();

            return action.ActionAgainst;
        }
    }
}