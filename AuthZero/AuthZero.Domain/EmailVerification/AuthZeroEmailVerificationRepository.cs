using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using AuthZero.Domain.Proxies;
using AuthZero.Interfaces;
using Common;

namespace AuthZero.Domain.EmailVerification
{
    /// <summary>
    /// Repository for auth0 email verfication
    /// </summary>
    public class AuthZeroEmailVerificationRepository : IAuthZeroEmailVerificationRepository
    {
        private readonly IAppSettings _appSettings;
        private readonly ManagementApiClient _authManagement;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthZeroEmailVerificationRepository"/> class.
        /// </summary>
        /// <param name="appSettings">The application settings.</param>
        public AuthZeroEmailVerificationRepository(IAppSettings appSettings)
        {
            _appSettings = appSettings;
            _authManagement = AuthZeroManagementClientFactory.Create(appSettings);
        }

        /// <summary>
        /// Creates an email verification ticket for
        /// </summary>
        /// <param name="globalId">The global identifier.</param>
        /// <returns></returns>
        public string Create(string globalId)
        {
            var request = new EmailVerificationTicketRequest
            {
                ResultUrl = _appSettings.GetWebsiteUrl(),
                UserId = globalId
            };
            return _authManagement.Tickets.CreateEmailVerificationTicketAsync(request)
                .Result.Value;
        }
    }
}