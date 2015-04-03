using System.Collections.Generic;
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

            api.SendMessageAsync(new EmailMessage
            {
                from_email = EmailAddress,
                to = new List<EmailAddress>
                {
                    new EmailAddress(EmailAddress)
                },
                text = notification.EmailBody,
                subject = notification.Subject
            });
        }
    }
}
