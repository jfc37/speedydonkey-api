using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace Notification.Notifications
{
    public class UserEnroledInBlock : INotification
    {
        public string EmailTo { get; }
        public string Subject => "Block Enrolment Confirmation";
        public string EmailBody { get; set; }
        public string TemplateName => "19376af6-a639-4a68-9d75-1103220c643e";
        public IList<KeyValuePair<string, string>> TemplateContent { get; set; }
        User User { get; set; }

        public UserEnroledInBlock(User user)
        {
            EmailTo = user.Email;
            User = user;

            TemplateContent = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("first_name", user.FirstName),
                new KeyValuePair<string, string>("surname", user.Surname)
            };
            if (user.EnroledBlocks != null && user.EnroledBlocks.Any())
            {
                TemplateContent.Add(new KeyValuePair<string, string>("blocks", String.Join(", ", user.EnroledBlocks.Select(x => x.Name))));
            }
        }
    }
}