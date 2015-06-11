using System.Collections.Generic;
using System.Linq;
using Action;
using Actions;
using Common;
using Data.Repositories;
using Models;
using Notification.NotificationHandlers;
using Notification.Notifications;

namespace ActionHandlers.CreateHandlers
{
    public class CreateAnnouncementHandler : CreateEntityHandler<CreateAnnouncement, Announcement>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Block> _blockRepository;
        private readonly INotificationHandler<EmailAnnouncement> _notificationHandler;

        public CreateAnnouncementHandler(IRepository<Announcement> repository, IRepository<User> userRepository, IRepository<Block> blockRepository, INotificationHandler<EmailAnnouncement> notificationHandler) : base(repository)
        {
            _userRepository = userRepository;
            _blockRepository = blockRepository;
            _notificationHandler = notificationHandler;
        }

        protected override void PreHandle(ICrudAction<Announcement> action)
        {
            if (action.ActionAgainst.Receivers != null)
            {
                var actualBlocks = action.ActionAgainst.Receivers.Select(b => _blockRepository.Get(b.Id)).Cast<IBlock>().ToList();
                action.ActionAgainst.Receivers = actualBlocks;   
            }
        }

        protected override void PostHandle(ICrudAction<Announcement> action, Announcement result)
        {
            if (result.Type.EqualsEnum(AnnouncementType.Email))
            {
                var usersToNotifiy = GetUsersToSendTo(result);
                foreach (var user in usersToNotifiy)
                {
                    _notificationHandler.Handle(new EmailAnnouncement(user, result.Message));
                }
            }
        }

        private IEnumerable<User> GetUsersToSendTo(Announcement announcement)
        {
            var users = new List<User>();
            if (announcement.NotifyAll)
            {
                users.AddRange(_userRepository.GetAll());
            }
            else
            {
                foreach (var receiver in announcement.Receivers)
                {
                    users.AddRange(receiver.EnroledStudents.OfType<User>());
                }
            }
            return users;
        }
    }
}
