using System;
using System.Collections.Generic;
using System.Linq;
using Mandrill;

namespace Notification
{

    public interface IMailSender
    {
        void Send();
    }
    public class MailSender : IMailSender
    {
        private const string EmailAddress = "speedydonkeydaddy@gmail.com";

        public void Send()
        {
            MandrillApi api = new MandrillApi("C4BfEL5RxHgoM3ynsq-S6g");

            var sendTask = api.SendMessageAsync(new EmailMessage
            {
                from_email = EmailAddress,
                to = new List<EmailAddress>
                {
                    new EmailAddress(EmailAddress)
                },
                text = "Hello there!",
                subject = "Testing"
            });
        }
    }
}
