using Notification.Notifications;

namespace Notification.NotificationHandlers
{
    public class UserEnroledInBlockHandler : NotificationHandler<UserEnroledInBlock>
    {
        public UserEnroledInBlockHandler(IMailMan mailMan)
            : base(mailMan)
        {
        }
    }
}
