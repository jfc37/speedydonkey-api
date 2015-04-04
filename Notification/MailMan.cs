using System.Collections.Generic;
using System.Linq;
using Mandrill;
using Notification.Notifications;

namespace Notification
{
    public interface IMailMan
    {
        void Send(INotification notification);
    }
    public class MailMan : IMailMan
    {
        private const string EmailAddress = "speedydonkeydaddy@gmail.com";
        private const string ApiKey = "C4BfEL5RxHgoM3ynsq-S6g";

        public void Send(INotification notification)
        {
            MandrillApi api = new MandrillApi(ApiKey);

            var templateContents = notification.TemplateContent.Select(x => new TemplateContent
            {
                name = x.Key,
                content = x.Value
            }).ToList();

            var emailMessage = new EmailMessage
            {
                from_email = EmailAddress,
                to = new List<EmailAddress>
                {
                    new EmailAddress(EmailAddress)
                },
                subject = notification.Subject,
                merge_language = "handlebars"
            };
            foreach (var templateContent in notification.TemplateContent)
            {
                emailMessage.AddGlobalVariable(templateContent.Key, templateContent.Value);
            }
            api.SendMessageAsync(emailMessage, notification.TemplateName, templateContents);
        }
    }
}
