using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace Notification.Notifications
{
    public class UserEnroledInBlock : INotification
    {
        public string EmailTo { get; private set; }
        public string Subject { get { return "Block Enrolment Confirmation"; } }
        public string EmailBody { get; set; }
        public string TemplateName { get { return "Block Enrolment"; } }
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
                TemplateContent.Add(new KeyValuePair<string, string>("blocks", String.Join(",", user.EnroledBlocks.Select(x => x.Name))));
            }
        }
    }
}