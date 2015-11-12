using System;
using System.Linq;
using Actions;
using Common;
using Common.Extensions;
using Data.Repositories;
using Models;
using Notification;
using Notification.Notifications;

namespace ActionHandlers.CreateHandlers
{
    public class CreateUserHandler : CreateEntityHandler<CreateUser, User>
    {
        private readonly IRepository<User> _repository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPostOffice _postOffice;
        private readonly IAppSettings _appSettings;

        public CreateUserHandler(IRepository<User> repository, IPasswordHasher passwordHasher, IPostOffice postOffice, IAppSettings appSettings) : base(repository)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _postOffice = postOffice;
            _appSettings = appSettings;
        }

        protected override void PreHandle(ICrudAction<User> action)
        {
            action.ActionAgainst.Password = _passwordHasher.CreateHash(action.ActionAgainst.Password);
            action.ActionAgainst.Claims = "";

            action.ActionAgainst.Status = GetUserStatus();
            action.ActionAgainst.ActivationKey = Guid.NewGuid();

            if (IsEmailOnAdminWhitelist(action.ActionAgainst.Email))
            {
                var allClaims = Enum.GetValues(typeof (Claim)).Cast<Claim>().ToList();
                allClaims.Remove(Claim.Invalid);
                action.ActionAgainst.Claims = String.Join(",", allClaims);

                if (Convert.ToBoolean(_appSettings.GetSetting(AppSettingKey.AutoActivateAdmin)))
                    action.ActionAgainst.Status = UserStatus.Active;
            }

        }

        private UserStatus GetUserStatus()
        {
            return Convert.ToBoolean(_appSettings.GetSetting(AppSettingKey.AutoActivateUser)) ? UserStatus.Active : UserStatus.Unactiviated;
        }

        private bool IsEmailOnAdminWhitelist(string email)
        {
            return _appSettings.GetSetting(AppSettingKey.AdminEmailWhitelist).Contains(String.Format("|{0}|", email));
        }

        protected override void PostHandle(ICrudAction<User> action, User result)
        {
            var userRegistered = new UserRegistered(result, _appSettings.GetWebsiteUrl());
            _postOffice.Send(userRegistered);

            result.GlobalId = "auth0|{0}".FormatWith(result.Id);
            _repository.Update(result);
        }
    }
}
