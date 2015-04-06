using System;
using System.Collections.Generic;
using Models;

namespace Notification.Notifications
{
    public class UserForgotPassword : INotification
    {
        public string EmailTo { get; private set; }
        public string Subject { get { return "Forgotten Password"; } }
        public string TemplateName { get { return "Forgotten Password"; } }
        public IList<KeyValuePair<string, string>> TemplateContent { get; set; }
        User User { get; set; }

        public UserForgotPassword(User user)
        {
            EmailTo = user.Email;
            User = user;

            TemplateContent = new[]
            {
                new KeyValuePair<string, string>("first_name", user.FirstName),
                new KeyValuePair<string, string>("surname", user.Surname),
                new KeyValuePair<string, string>("reset", String.Format("https://spa-speedydonkey.azurewebsites.net/#/account/{0}/password/reset", user.ActivationKey)), 
            };
        }
    }
}