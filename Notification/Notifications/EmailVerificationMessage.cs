using System.Collections.Generic;
using Common;
using Common.Extensions;
using Models;

namespace Notification.Notifications
{
    public class EmailVerificationMessage : INotification
    {
        public string EmailTo { get; }
        public string Subject { get; }
        public string TemplateName => "Email Verification";
        public IList<KeyValuePair<string, string>> TemplateContent { get; set; }

        public EmailVerificationMessage(IAppSettings appSettings, User user, string emailTicket)
        {
            var applicationName = appSettings.GetSetting(AppSettingKey.ApplicationName);
            Subject = $"{applicationName} - Email Verification";

            EmailTo = user.Email;

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
}