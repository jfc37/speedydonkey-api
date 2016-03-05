using System;
using System.Linq;
using Action;
using Common;
using Data.Repositories;
using Models;
using Notification;
using Notification.Notifications;

namespace ActionHandlers
{
    public class ForgottenPasswordHandler : IActionHandler<ForgottenPassword, User>
    {
        private readonly IRepository<User> _repository;
        private readonly IPostOffice _postOffice;
        private readonly IAppSettings _appSettings;

        public ForgottenPasswordHandler(IRepository<User> repository, IPostOffice postOffice, IAppSettings appSettings)
        {
            _repository = repository;
            _postOffice = postOffice;
            _appSettings = appSettings;
        }

        public User Handle(ForgottenPassword action)
        {
            var user = _repository.Queryable().Single(x => x.Email == action.ActionAgainst.Email);
            user.ActivationKey = Guid.NewGuid();
            _repository.Update(user);

            _postOffice.Send(new UserForgotPassword(user, _appSettings.GetWebsiteUrl()));

            return user;
        }
    }
}
