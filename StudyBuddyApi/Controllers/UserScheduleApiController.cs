using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class UserScheduleApiController : BaseApiController
    {
        private readonly IAdvancedRepository<User, IList<IEvent>> _userRepository;
        private readonly IUrlConstructor _urlConstructor;
        private readonly ICommonInterfaceCloner _cloner;
        private readonly ICurrentUser _currentUser;

        public UserScheduleApiController(
            IAdvancedRepository<User, IList<IEvent>> userRepository,
            IUrlConstructor urlConstructor,
            ICommonInterfaceCloner cloner,
            ICurrentUser currentUser)
        {
            _userRepository = userRepository;
            _urlConstructor = urlConstructor;
            _cloner = cloner;
            _currentUser = currentUser;
        }

        [ActiveUserRequired]
        public HttpResponseMessage Get()
        {
            return Get(_currentUser.Id);
        }

        [ClaimsAuthorise(Claim = Claim.AnyUserData)]
        public HttpResponseMessage Get(int id)
        {
            var userSchedule = _userRepository.Get(id);

            if (userSchedule == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return userSchedule.Any()
                ? Request.CreateResponse(userSchedule.OfType<Class>().Select(x => new ClassModel().CloneFromEntity(Request, _urlConstructor, x, _cloner)))
                : Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}