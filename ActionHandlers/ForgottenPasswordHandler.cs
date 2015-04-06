using System;
using System.Linq;
using Action;
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

        public ForgottenPasswordHandler(IRepository<User> repository, IPostOffice postOffice)
        {
            _repository = repository;
            _postOffice = postOffice;
        }

        public User Handle(ForgottenPassword action)
        {
            var user = _repository.GetAll().Single(x => x.Email == action.ActionAgainst.Email);
            user.ActivationKey = Guid.NewGuid();
            _repository.Update(user);

            _postOffice.Send(new UserForgotPassword(user));

            return user;
        }
    }
}
