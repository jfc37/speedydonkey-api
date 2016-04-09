using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Extensions;
using Models;

namespace Notification.Notifications
{
    public class EmailVerificationMessage : INotification
    {
        public string EmailTo { get; }
        public string Subject { get; }
        public string TemplateName => "9a5a9d27-f31c-4f69-93af-ced633456195";
        public IList<KeyValuePair<string, string>> TemplateContent { get; set; }

        public EmailVerificationMessage(IAppSettings appSettings, User user, string emailTicket)
        {
            var applicationName = appSettings.GetSetting(AppSettingKey.ApplicationName);
            Subject = $"{applicationName} - Email Verification";

            EmailTo = user.Email;

            TemplateContent = new[]
            {
                new KeyValuePair<string, string>("ticket", emailTicket),
            }.ToList();
        }

        public override string ToString()
        {
            return this.ToDebugString(nameof(Subject), nameof(EmailTo));
        }
    }
}