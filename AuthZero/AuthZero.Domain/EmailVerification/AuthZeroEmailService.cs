using System.Linq;
using AuthZero.Interfaces;
using Common;
using Data.Repositories;
using Models;
using Notification;
using Notification.Notifications;

namespace AuthZero.Domain.EmailVerification
{
    /// <summary>
    /// Email services for auth0
    /// </summary>
    public class AuthZeroEmailService : IAuthZeroEmailService
    {
        private readonly IAuthZeroEmailVerificationRepository _emailVerificationRepository;
        private readonly IPostOffice _postOffice;
        private readonly IRepository<User> _repository;
        private readonly IAppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthZeroEmailService"/> class.
        /// </summary>
        /// <param name="emailVerificationRepository">The email verification repository.</param>
        /// <param name="postOffice">The post office.</param>
        /// <param name="repository">The repository.</param>
        /// <param name="appSettings">The application settings.</param>
        public AuthZeroEmailService(
            IAuthZeroEmailVerificationRepository emailVerificationRepository,
            IPostOffice postOffice,
            IRepository<User> repository,
            IAppSettings appSettings)
        {
            _emailVerificationRepository = emailVerificationRepository;
            _postOffice = postOffice;
            _repository = repository;
            _appSettings = appSettings;
        }

        /// <summary>
        /// Sends an verification email for a given auth0 user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public void SendVerification(string userId)
        {
            var user = _repository.Queryable()
                .Single(x => x.GlobalId == userId);

            var emailTicket = _emailVerificationRepository.Create(userId);
            var emailVerification = new EmailVerificationMessage(_appSettings, user, emailTicket);

            _postOffice.Send(emailVerification);
        }
    }
}
