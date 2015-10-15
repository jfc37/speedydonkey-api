using System;
using System.Collections.Generic;
using System.Linq;
using Common;
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
        private readonly IAppSettings _appSettings;

        public MailMan(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public void Send(INotification notification)
        {
            if (!Convert.ToBoolean(_appSettings.GetSetting(AppSettingKey.ShouldSendEmail)))
                return;

            var api = new MandrillApi(_appSettings.GetSetting(AppSettingKey.MandrillApiKey));

            var templateContents = notification.TemplateContent.Select(x => new TemplateContent
            {
                name = x.Key,
                content = x.Value
            }).ToList();

            var emailMessage = new EmailMessage
            {
                from_email = _appSettings.GetSetting(AppSettingKey.FromEmail),
                to = new List<EmailAddress>
                {
                    new EmailAddress(GetEmailTo(notification.EmailTo))
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

        private string GetEmailTo(string realEmail)
        {
            if (Convert.ToBoolean(_appSettings.GetSetting(AppSettingKey.UseRealEmail)))
                return realEmail;

            return _appSettings.GetSetting(AppSettingKey.TestEmailAccount);
        }
    }
}
