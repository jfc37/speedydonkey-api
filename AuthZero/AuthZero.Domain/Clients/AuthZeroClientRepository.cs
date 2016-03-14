using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using AuthZero.Domain.Extensions;
using AuthZero.Domain.Proxies;
using AuthZero.Interfaces;
using Common;
using Models;

namespace AuthZero.Domain.Clients
{
    /// <summary>
    /// Repository for users from Auth0
    /// </summary>
    public class AuthZeroClientRepository : IAuthZeroClientRepository
    {
        private readonly IAppSettings _appSettings;
        private readonly ManagementApiClient _authManagement;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthZeroClientRepository"/> class.
        /// </summary>
        /// <param name="appSettings">The application settings.</param>
        public AuthZeroClientRepository(IAppSettings appSettings)
        {
            _appSettings = appSettings;
            _authManagement = AuthZeroManagementClientFactory.Create(appSettings);
        }

        /// <summary>
        /// Creates the user in auth0, returning the auth0 user id
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public string Create(User user)
        {
            var userCreateRequest = new UserCreateRequest
            {
                Email = user.Email,
                Password = user.Password,
                EmailVerified = false,
                Connection = _appSettings.GetSetting(AppSettingKey.AuthZeroConnection)
            };

            var authUser = _authManagement.Users.CreateAsync(userCreateRequest).Result;

            return authUser.UserId;
        }

        public User Get(string globalId)
        {
            return _authManagement.Users.GetAsync(globalId)
                .Result
                .ToUser();
        }

        public void Delete(string globalId)
        {
            _authManagement.Users.DeleteAsync(globalId);
        }
    }
}