using System.Collections.Generic;
using Common.Extensions;
using Models;

namespace Notification.Notifications
{
    public class EmailVerificationMessage : INotification
    {
        public string EmailTo { get; }
        public string Subject => "Email Verification";
        public string TemplateName => "Email Verification";
        public IList<KeyValuePair<string, string>> TemplateContent { get; set; }

        public EmailVerificationMessage(User user, string emailTicket)
        {
            TemplateContent = new[]
            {
                new KeyValuePair<string, string>("first_name", user.FirstName),
                new KeyValuePair<string, string>("surname", user.Surname),
                new KeyValuePair<string, string>("ticket", emailTicket),
            };
        }

        public override string ToString()
        {
            return this.ToDebugString(nameof(Subject), nameof(EmailTo));
        }
    }

    public class EmailAnnouncement : INotification
    {
        public string EmailTo { get; private set; }
        public string Subject { get; private set; }
        public string TemplateName { get { return "Announcement"; } }
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