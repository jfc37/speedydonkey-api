using System.Collections.Generic;
using Models;
using Newtonsoft.Json;

namespace Notification.Notifications
{
    public class WindyLindyRegistrationCompletion : INotification
    {
        public string EmailTo { get; private set; }
        public string Subject { get { return "Windy Lindy Registration"; } }
        public string TemplateName { get { return "Windy Lindy Registration"; } }
        public IList<KeyValuePair<string, string>> TemplateContent { get; set; }
        Registration Registration{ get; set; }

        public WindyLindyRegistrationCompletion(Registration registration)
        {
            EmailTo = registration.Email;
            Registration = registration;

            TemplateContent = new[]
            {
                new KeyValuePair<string, string>("pass_type", registration.GetDescription())
            };
        }
    }
}