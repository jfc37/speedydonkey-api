using System.Linq;
using System.Web.Http;
using ActionHandlers.CreateHandlers;
using Common;
using Common.Extensions;
using Contracts.Users;
using Data.Repositories;
using Models;
using Notification;
using Notification.Notifications;

namespace SpeedyDonkeyApi.Controllers.Emails
{
    [RoutePrefix("api/emails/verification")]
    public class AuthZeroApiController : ApiController
    {
        private readonly IAuthZeroEmailService _authZeroEmailService;

        public AuthZeroApiController(IAuthZeroEmailService authZeroEmailService)
        {
            _authZeroEmailService = authZeroEmailService;
        }

        [Route]
        //[Authorize]
        public IHttpActionResult Verification(AuthZeroUserModel model)
        {
            _authZeroEmailService.SendVerification(model.UserId);

            return Ok();
        }
    }
    

    public interface IAuthZeroEmailService
    {
        void SendVerification(string userId);
    }

    public class AuthZeroEmailService : IAuthZeroEmailService
    {
        private readonly IAuthZeroEmailVerificationRepository _emailVerificationRepository;
        private readonly IAuthZeroClientRepository _authZeroClientRepository;
        private readonly IPostOffice _postOffice;
        private readonly IRepository<User> _repository;
        private readonly IAppSettings _appSettings;

        public AuthZeroEmailService(
            IAuthZeroEmailVerificationRepository emailVerificationRepository,
            IAuthZeroClientRepository authZeroClientRepository,
            IPostOffice postOffice,
            IRepository<User> repository,
            IAppSettings appSettings)
        {
            _emailVerificationRepository = emailVerificationRepository;
            _authZeroClientRepository = authZeroClientRepository;
            _postOffice = postOffice;
            _repository = repository;
            _appSettings = appSettings;
        }

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
