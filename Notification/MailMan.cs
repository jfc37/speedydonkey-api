using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Mandrill;
using Mandrill.Models;
using Mandrill.Requests.Messages;
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
                Name = x.Key,
                Content = x.Value
            }).ToList();

            var emailMessage = new EmailMessage
            {
                FromEmail = _appSettings.GetSetting(AppSettingKey.FromEmail),
                To = new List<EmailAddress>
                {
                    new EmailAddress(GetEmailTo(notification.EmailTo))
                },
                Subject = notification.Subject,
                MergeLanguage = "handlebars"
            };
            foreach (var templateContent in notification.TemplateContent)
            {
                emailMessage.AddGlobalVariable(templateContent.Key, templateContent.Value);
            }
            api.SendMessageTemplate(new SendMessageTemplateRequest(emailMessage, notification.TemplateName, templateContents));
        }

        private string GetEmailTo(string realEmail)
        {
            if (Convert.ToBoolean(_appSettings.GetSetting(AppSettingKey.UseRealEmail)))
                return realEmail;

            return _appSettings.GetSetting(AppSettingKey.TestEmailAccount);
        }
    }
}
