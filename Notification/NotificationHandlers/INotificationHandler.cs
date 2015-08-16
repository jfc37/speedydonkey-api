using Notification.Notifications;

namespace Notification.NotificationHandlers
{
    public interface INotificationHandler<in T> where T : INotification
    {
        void Handle(T notification);
    }
}