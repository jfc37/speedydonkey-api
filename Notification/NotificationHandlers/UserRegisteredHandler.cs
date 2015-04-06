using Notification.Notifications;

namespace Notification.NotificationHandlers
{
    public class UserRegisteredHandler : NotificationHandler<UserRegistered>
    {
        public UserRegisteredHandler(IMailMan mailMan) : base(mailMan)
        {
        }
    }
    public class UserForgotPasswordHandler : NotificationHandler<UserForgotPassword>
    {
        public UserForgotPasswordHandler(IMailMan mailMan)
            : base(mailMan)
        {
        }
    }
}
