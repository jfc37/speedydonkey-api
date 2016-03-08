using System.Linq;
using System.Web.Http;
using ActionHandlers.CreateHandlers;
using Common.Extensions;
using Data.Repositories;
using Models;
using Notification;
using Notification.Notifications;

namespace SpeedyDonkeyApi.Controllers.Emails
{
    [RoutePrefix("api/emails/verification")]
    public class VerificationApiController : ApiController
    {
        private readonly IEmailVerification _emailVerification;

        public VerificationApiController(IEmailVerification emailVerification)
        {
            _emailVerification = emailVerification;
        }

        [Route]
        public IHttpActionResult Post(string userId, string email)
        {
            _emailVerification.SendVerification(userId);

            return Ok();
        }
    }

    public interface IEmailVerification
    {
        void SendVerification(string userId);
    }

    public class EmailVerification : IEmailVerification
    {
        private readonly IAuthZeroEmailVerificationRepository _emailVerificationRepository;
        private readonly IAuthZeroClientRepository _authZeroClientRepository;
        private readonly IPostOffice _postOffice;
        private readonly IRepository<User> _repository;

        public EmailVerification(
            IAuthZeroEmailVerificationRepository emailVerificationRepository,
            IAuthZeroClientRepository authZeroClientRepository,
            IPostOffice postOffice,
            IRepository<User> repository)
        {
            _emailVerificationRepository = emailVerificationRepository;
            _authZeroClientRepository = authZeroClientRepository;
            _postOffice = postOffice;
            _repository = repository;
        }

        public void SendVerification(string userId)
        {
            var userNeedsToBeCreated = _repository.Queryable()
                .NotAny(x => x.GlobalId == userId);

            if (userNeedsToBeCreated)
            {
                var user = _authZeroClientRepository.Get(userId);
                _repository.Create(user);
            }

            var domainUser = _repository.Queryable()
                .Single(x => x.GlobalId == userId);
            var emailTicket = _emailVerificationRepository.Create(userId);

            var emailVerification = new EmailVerificationMessage(domainUser, emailTicket);
            _postOffice.Send(emailVerification);
        }
    }
}
