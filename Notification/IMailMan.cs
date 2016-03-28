using Notification.Notifications;

namespace Notification
{
    public interface IMailMan
    {
        void Send(INotification notification);
    }
}