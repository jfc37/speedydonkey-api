using System;
using System.Collections.Generic;
using Models;

namespace Notification.Notifications
{
    public class UserRegistered : INotification
    {
        public string EmailTo { get; private set; }
        public string Subject { get { return "Welcome to Full Swing!"; } }
        public string TemplateName { get { return "New User"; } }
        public IList<KeyValuePair<string, string>> TemplateContent { get; set; }
        IUser User { get; set; }

        public UserRegistered(IUser user)
        {
            EmailTo = user.Email;
            User = user;

            TemplateContent = new[]
            {
                new KeyValuePair<string, string>("first_name", user.FirstName),
                new KeyValuePair<string, string>("surname", user.Surname),
            };
        }
    }
}