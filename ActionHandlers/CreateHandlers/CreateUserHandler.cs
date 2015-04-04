using System;
using System.Linq;
using Actions;
using Data.Repositories;
using Models;
using Notification;
using Notification.Notifications;

namespace ActionHandlers.CreateHandlers
{
    public class CreateUserHandler : CreateEntityHandler<CreateUser, User>
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPostOffice _postOffice;

        public CreateUserHandler(IRepository<User> repository, IPasswordHasher passwordHasher, IPostOffice postOffice) : base(repository)
        {
            _passwordHasher = passwordHasher;
            _postOffice = postOffice;
        }

        protected override void PreHandle(ICrudAction<User> action)
        {
            action.ActionAgainst.Password = _passwordHasher.CreateHash(action.ActionAgainst.Password);
            if (action.ActionAgainst.Email.EndsWith("fullswing.co.nz"))
            {
                var allClaims = Enum.GetValues(typeof (Claim)).Cast<Claim>().ToList();
                allClaims.Remove(Claim.Invalid);
                action.ActionAgainst.Claims = String.Join(",", allClaims);
                action.ActionAgainst.Status = UserStatus.Active;
            }
            else
                action.ActionAgainst.Status = UserStatus.Unactiviated;

            action.ActionAgainst.ActivationKey = Guid.NewGuid();
        }

        protected override void PostHandle(ICrudAction<User> action, User result)
        {
            var userRegistered = new UserRegistered(result);
            _postOffice.Send(userRegistered);
        }
    }
}
