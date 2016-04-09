using System.Collections.Generic;
using Common.Extensions;
using Models;

namespace Notification.Notifications
{
    public class EmailAnnouncement : INotification
    {
        public string EmailTo { get; }
        public string Subject { get; }
        public string TemplateName => "bf054b3c-8d47-48cb-8c55-ba39c232fc3f";
        public IList<KeyValuePair<string, string>> TemplateContent { get; set; }
        User User { get; set; }

        public EmailAnnouncement(User user, string message, string subject)
        {
            EmailTo = user.Email;
            Subject = subject;
            User = user;

            TemplateContent = new[]
            {
                new KeyValuePair<string, string>("first_name", user.FirstName),
                new KeyValuePair<string, string>("surname", user.Surname),
                new KeyValuePair<string, string>("message", message) 
            };
        }
        
        public override string ToString()
        {
            return this.ToDebugString(nameof(Subject), nameof(EmailTo));
        }
    }
}