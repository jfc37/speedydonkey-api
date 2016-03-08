using System;
using System.Linq;
using Actions;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Common;
using Data.Repositories;
using Models;
using User = Models.User;

namespace ActionHandlers.CreateHandlers
{
    public interface IAuthZeroClientRepository
    {
        string Create(User user);

        User Get(string globalId);
    }
    public interface IAuthZeroEmailVerificationRepository
    {
        string Create(string globalId);
    }

    public class AuthZeroEmailVerificationRepository : IAuthZeroEmailVerificationRepository
    {
        private readonly IAppSettings _appSettings;
        private readonly ManagementApiClient _authManagement;

        public AuthZeroEmailVerificationRepository(IAppSettings appSettings)
        {
            _appSettings = appSettings;
            _authManagement = AuthZeroManagementClientFactory.Create(appSettings);
        }

        public string Create(string globalId)
        {
            var request = new EmailVerificationTicketRequest
            {
                ResultUrl = _appSettings.GetWebsiteUrl(),
                UserId = globalId
            };
            return _authManagement.Tickets.CreateEmailVerificationTicket(request)
                .Result.Value;
        }
    }

    public static class AuthZeroManagementClientFactory
    {
        public static ManagementApiClient Create(IAppSettings appSettings)
        {
            var jwt = appSettings.GetSetting(AppSettingKey.AuthZeroToken);
            var api = new Uri($"https://{appSettings.GetSetting(AppSettingKey.AuthZeroDomain)}/api/v2");

            return new ManagementApiClient(jwt, api);
        }
    }

    public class AuthZeroClientRepository : IAuthZeroClientRepository
    {
        private readonly IAppSettings _appSettings;
        private readonly ManagementApiClient _authManagement;

        public AuthZeroClientRepository(IAppSettings appSettings)
        {
            _appSettings = appSettings;
            _authManagement = AuthZeroManagementClientFactory.Create(appSettings);
        }

        public string Create(User user)
        {
            var userCreateRequest = new UserCreateRequest
            {
                Email = user.Email,
                Password = user.Password,
                EmailVerified = false,
                Connection = _appSettings.GetSetting(AppSettingKey.AuthZeroConnection)
            };

            var authUser = _authManagement.Users.Create(userCreateRequest).Result;

            return authUser.UserId;
        }

        public User Get(string globalId)
        {
            return _authManagement.Users.Get(globalId)
                .Result
                .ToUser();
        }
    }

    public static class AuthZeroUserExtensions
    {
        public static User ToUser(this Auth0.Core.User instance)
        {
            return new User
            {
                Email = instance.Email,
                FirstName = instance.FirstName,
                Surname = instance.LastName,
                GlobalId = instance.UserId
            };
        }
    }

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
