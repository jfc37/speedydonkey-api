using System;
using System.Linq;
using Actions;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Common;
using Data.Repositories;
using Models;
using Notification;
using User = Models.User;

namespace ActionHandlers.CreateHandlers
{
    public interface IAuthZeroClientRepository
    {
        string Create(User user);
    }

    public class AuthZeroClientRepository : IAuthZeroClientRepository
    {
        private readonly ManagementApiClient _authManagement;

        public AuthZeroClientRepository(IAppSettings appSettings)
        {
            var jwt = appSettings.GetSetting(AppSettingKey.AuthToken);
            var api = new Uri($"https://{appSettings.GetSetting(AppSettingKey.AuthZeroDomain)}/api/v2");
            _authManagement = new ManagementApiClient(jwt, api);
            
        }

        public string Create(User user)
        {
            var userCreateRequest = new UserCreateRequest
            {
                Email = user.Email,
                Password = user.Password,
                EmailVerified = false,
                Connection = "speedydonkeydb"
            };

            var authUser = _authManagement.Users.Create(userCreateRequest).Result;

            return authUser.UserId;
        }
    }

    public class CreateUserHandler : CreateEntityHandler<CreateUser, User>
    {
        private readonly IRepository<User> _repository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPostOffice _postOffice;
        private readonly IAppSettings _appSettings;
        private readonly IAuthZeroClientRepository _authZeroClientRepository;

        public CreateUserHandler(
            IRepository<User> repository, 
            IPasswordHasher passwordHasher, 
            IPostOffice postOffice, 
            IAppSettings appSettings,
            IAuthZeroClientRepository authZeroClientRepository) : base(repository)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _postOffice = postOffice;
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
