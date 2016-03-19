using System;
using System.Linq;
using Action.Users;
using AuthZero.Interfaces;
using Common;
using Data.Repositories;
using Models;
using User = Models.User;

namespace ActionHandlers.Users
{
    public class CreateUserFromAuthZeroHandler : IActionHandler<CreateUserFromAuthZero, User>
    {
        private readonly IRepository<User> _repository;
        private readonly IAppSettings _appSettings;
        private readonly IAuthZeroClientRepository _authZeroClientRepository;

        public CreateUserFromAuthZeroHandler(
            IRepository<User> repository, 
            IAppSettings appSettings,
            IAuthZeroClientRepository authZeroClientRepository)
        {
            _repository = repository;
            _appSettings = appSettings;
            _authZeroClientRepository = authZeroClientRepository;
        }

        public User Handle(CreateUserFromAuthZero action)
        {
            var authZeroUser = _authZeroClientRepository.Get(action.ActionAgainst.GlobalId);

            if (IsEmailOnAdminWhitelist(authZeroUser.Email))
            {
                var allClaims = Enum.GetValues(typeof(Claim)).Cast<Claim>().ToList();
                allClaims.Remove(Claim.Invalid);
                authZeroUser.Claims = String.Join(",", allClaims);
            }

            return _repository.Create(authZeroUser);
        }

        private bool IsEmailOnAdminWhitelist(string email)
        {
            return _appSettings.GetSetting(AppSettingKey.AdminEmailWhitelist).Contains($"|{email}|");
        }
    }
}
