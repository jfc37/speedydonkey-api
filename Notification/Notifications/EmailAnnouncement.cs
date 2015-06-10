using System.Collections.Generic;
using Models;

namespace Notification.Notifications
{
    public class EmailAnnouncement : INotification
    {
        public string EmailTo { get; private set; }
        public string Subject { get { return "Announcement from Full Swing"; } }
        public string TemplateName { get { return "Announcement"; } }
        public IList<KeyValuePair<string, string>> TemplateContent { get; set; }
        User User { get; set; }

        public EmailAnnouncement(User user, string message)
        {
            EmailTo = user.Email;
            User = user;

            TemplateContent = new[]
            {
                new KeyValuePair<string, string>("first_name", user.FirstName),
                new KeyValuePair<string, string>("surname", user.Surname),
                new KeyValuePair<string, string>("message", message) 
            };
        }
    }
}