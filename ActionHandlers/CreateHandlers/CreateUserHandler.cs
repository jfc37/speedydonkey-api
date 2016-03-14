using System;
using System.Linq;
using Action.Users;
using Actions;
using AuthZero.Interfaces;
using Common;
using Data.Repositories;
using Models;
using User = Models.User;

namespace ActionHandlers.CreateHandlers
{
    public class CreateUserHandler : CreateEntityHandler<CreateUser, User>
    {
        private readonly IAppSettings _appSettings;
        private readonly IAuthZeroClientRepository _authZeroClientRepository;

        public CreateUserHandler(
            IRepository<User> repository, 
            IAppSettings appSettings,
            IAuthZeroClientRepository authZeroClientRepository) : base(repository)
        {
            _appSettings = appSettings;
            _authZeroClientRepository = authZeroClientRepository;
        }

        protected override void PreHandle(ICrudAction<User> action)
        {
            action.ActionAgainst.GlobalId = _authZeroClientRepository.Create(action.ActionAgainst);

            action.ActionAgainst.Password = "";
            action.ActionAgainst.Claims = "";

            if (IsEmailOnAdminWhitelist(action.ActionAgainst.Email))
            {
                var allClaims = Enum.GetValues(typeof (Claim)).Cast<Claim>().ToList();
                allClaims.Remove(Claim.Invalid);
                action.ActionAgainst.Claims = String.Join(",", allClaims);
            }
        }

        private bool IsEmailOnAdminWhitelist(string email)
        {
            return _appSettings.GetSetting(AppSettingKey.AdminEmailWhitelist).Contains($"|{email}|");
        }
    }
}
