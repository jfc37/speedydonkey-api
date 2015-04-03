using Notification.Notifications;

namespace Notification.NotificationHandlers
{
    public class UserRegisteredHandler : NotificationHandler<UserRegistered>
    {
        public UserRegisteredHandler(IMailMan mailMan) : base(mailMan)
        {
        }
    }
}
