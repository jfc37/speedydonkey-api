using System;
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
        IUser User { get; set; }

        public UserEnroledInBlock(IUser user)
        {
            EmailTo = user.Email;
            User = user;

            var sb = new StringBuilder(String.Format("Hello {0},", user.FullName));
            if (user.EnroledBlocks != null && user.EnroledBlocks.Any())
                sb.AppendLine(String.Format("You have enrolled in the following: {0}", String.Join(",", user.EnroledBlocks.Select(x => x.Name))));

            if (user.Passes != null && user.Passes.Any())
                sb.AppendLine(String.Format("You have purchased the following passes: {0}", String.Join(",", user.Passes.Select(x => x.PassType))));


            EmailBody = sb.ToString();
        }
    }
}