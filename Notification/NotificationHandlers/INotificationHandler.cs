using Notification.Notifications;

namespace Notification.NotificationHandlers
{
    public interface INotificationHandler<in T> where T : INotification
    {
        void Handle(T notification);
    }

    public class NotificationHandler<T> : INotificationHandler<T> where T : INotification
    {
        private readonly IMailMan _mailMan;

        public NotificationHandler(IMailMan mailMan)
        {
            _mailMan = mailMan;
        }

        public void Handle(T notification)
        {
            _mailMan.Send(notification);
        }
    }
}