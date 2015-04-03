using System;
using Models;

namespace Notification.Notifications
{
    public class UserRegistered : INotification
    {
        public string EmailTo { get; private set; }
        public string Subject { get { return "Welcome to Full Swing!"; } }
        public string EmailBody { get; set; }
        IUser User { get; set; }

        public UserRegistered(IUser user)
        {
            EmailTo = user.Email;
            User = user;

            EmailBody = String.Format("Hello {0}, welcome to Full Swing!", user.FullName);
        }
    }
}