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
                .Get(action.ActionAgainst);

            foreach (var user in usersToNotify)
            {
                _notificationHandler.Handle(new EmailAnnouncement(user, action.ActionAgainst.Message, action.ActionAgainst.Subject));
            }

            return action.ActionAgainst;
        }
    }
}