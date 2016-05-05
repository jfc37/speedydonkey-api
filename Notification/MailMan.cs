using System;
using System.Net.Mail;
using Common;
using Common.Extensions;
using Notification.Notifications;
using PostSharp.Patterns.Diagnostics;
using SendGrid;

namespace Notification
{
    public class MailMan : IMailMan
    {
        private readonly IAppSettings _appSettings;

        public MailMan(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        [Log]
        public void Send(INotification notification)
        {
            if (!Convert.ToBoolean(_appSettings.GetSetting(AppSettingKey.ShouldSendEmail)))
                return;

            var message = GetMessage(notification);

            SendMessage(message);
        }

        private void SendMessage(SendGridMessage message)
        {
            var transportWeb = new Web(_appSettings.GetSetting(AppSettingKey.SendGridApiKey));
            transportWeb.DeliverAsync(message);
        }

        private SendGridMessage GetMessage(INotification notification)
        {
            var applicationName = _appSettings.GetSetting(AppSettingKey.ApplicationName);

            var message = new SendGridMessage();
            message.From = new MailAddress(_appSettings.GetSetting(AppSettingKey.FromEmail), applicationName);
            message.Subject = notification.Subject;
            message.AddTo(GetEmailTo(notification.EmailTo));

            message.EnableTemplateEngine(notification.TemplateName);

            message.Text = "   ";

            foreach (var templateContent in notification.TemplateContent)
            {
                message.AddSubstitution(GetReplacementTag(templateContent.Key), templateContent.Value.PutIntoList());
            }
            message.AddSubstitution(GetReplacementTag("application_name"), applicationName.PutIntoList());
            return message;
        }

        private static string GetReplacementTag(string content)
        {
            return $"-{content}-";
        }

        private string GetEmailTo(string realEmail)
        {
            return Convert.ToBoolean(_appSettings.GetSetting(AppSettingKey.UseRealEmail)) 
                ? realEmail 
                : _appSettings.GetSetting(AppSettingKey.TestEmailAccount);
        }
    }
}
