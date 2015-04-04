using System.Collections.Generic;

namespace Notification.Notifications
{
    public interface INotification
    {
        string EmailTo { get; }
        string Subject { get; }
        string TemplateName { get; }
        IList<KeyValuePair<string, string>> TemplateContent { get; set; }
    }
}